using bolsadetrabajo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;

namespace bolsadetrabajo.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult LoginIn()

        {

            if(User.Identity.IsAuthenticated){
                var trab = _context.Trabajador.Where(t => t.Email.Equals(User.Identity.Name)).FirstOrDefault();
                var empr = _context.Empresa.Where(t => t.Email.Equals(User.Identity.Name)).FirstOrDefault();
                if (trab != null)
                {
                    return RedirectToAction("Index", "Trabajador");
                }
                else
                {
                    if (empr != null)
                    {
                        return RedirectToAction("Index", "Empresa");
                    }
                    else
                    {
                        return View();

                    }
                }
            }
            else
            {
                return View();

            }
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(LoginIn));
        }


        [HttpPost]
        public async Task<IActionResult> LoginInTrabajador(string CorreoTrabajador,string PasswordTrabajador)
        {
            
            //Trabajador
            var us = _context.Trabajador.Where(u => u.Email.Equals(CorreoTrabajador)).FirstOrDefault();
            if (us != null)
            {
                //Usuario Encontrado
                if (VerificarPass(PasswordTrabajador, us.PasswordHash, us.PasswordSalt))
                {
                    //Usuario y Contraseña Correctos!
                    var Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, CorreoTrabajador),
                        new Claim(ClaimTypes.NameIdentifier, CorreoTrabajador),
                        new Claim(ClaimTypes.Role, "Trabajador")
                    };

                    
                    var identity = new ClaimsIdentity(Claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties { IsPersistent = true }
                        );

                    return RedirectToAction("Index", "Trabajador");
                }
                else
                {
                    //Usuario correcto pero contraseña mala
                    ModelState.AddModelError("", "Contraseña Incorrecta");
                    return RedirectToAction("LoginIn");
                }


            }
            else
            {
                //Usuario No Existe
                ModelState.AddModelError("", "Usuario no Encontrado!");
                return RedirectToAction("LoginIn");

            }



        }





        [HttpPost]
        public IActionResult RegistrarTrabajador(string CorreoTrabajador_reg, string RutTrabajador_reg, string NombreTrabajador_reg, string DireccionTrabajador_reg, string FechaNacimientoTrabajador_reg, string PasswordTrabajador_reg)
        {
            //Trabajador
            var us = _context.Trabajador.Where(u => u.Email.Equals(CorreoTrabajador_reg)).FirstOrDefault();
            if (us != null)
            {
                //el usuario ya esta registrado con el corre ingresado
                ModelState.AddModelError("", "Correo Ya Registrado!");
                return View();
            }
            else
            {
                Trabajador T = new Trabajador();
                T.Nombre = NombreTrabajador_reg;
                T.Email = CorreoTrabajador_reg;
                T.Rut = RutTrabajador_reg;
                T.Direccion = DireccionTrabajador_reg;
                T.FechaNacimiento = FechaNacimientoTrabajador_reg;

                CreatePasswordHash(PasswordTrabajador_reg, out byte[] passwordHash, out byte[] passwordSalt);

                T.PasswordHash = passwordHash;
                T.PasswordSalt = passwordSalt;
                _context.Trabajador.Add(T);
                _context.SaveChanges();
                return RedirectToAction("LoginIn", "Auth");
            }

        }








        [HttpPost]
        public async Task<IActionResult> LoginInEmpresa(string CorreoEmpresa, string PasswordEmpresa)
        {

            //Trabajador
            var us = _context.Empresa.Where(u => u.Email.Equals(CorreoEmpresa)).FirstOrDefault();
            if (us != null)
            {
                //Usuario Encontrado
                if (VerificarPass(PasswordEmpresa, us.PasswordHash, us.PasswordSalt))
                {
                    //Usuario y Contraseña Correctos!
                    var Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, CorreoEmpresa),
                        new Claim(ClaimTypes.NameIdentifier, CorreoEmpresa),
                        new Claim(ClaimTypes.Role, "Empresa")
                    };

                    //Carnet, Licencia
                    var identity = new ClaimsIdentity(Claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties { IsPersistent = true }
                        );

                    return RedirectToAction("Index", "Empresa");
                }
                else
                {
                    //Usuario correcto pero contraseña mala
                    ModelState.AddModelError("", "Contraseña Incorrecta");
                    return RedirectToAction("LoginIn");
                }


            }
            else
            {
                //Usuario No Existe
                ModelState.AddModelError("", "Usuario no Encontrado!");
                return RedirectToAction("LoginIn");

            }



        }



        [HttpPost]
        public IActionResult RegistrarEmpresa(string CorreoEmpresa_reg, string RazonSocialEmpresa_reg, string RutRepresentanteEmpresa_reg, string PasswordEmpresa_reg)
        {
            //eMPRESA   
            var us = _context.Empresa.Where(u => u.Email.Equals(CorreoEmpresa_reg)).FirstOrDefault();
            if (us != null)
            {
                //la empresa ya esta registrado con el corre ingresado
                ModelState.AddModelError("", "Correo Ya Registrado!");
                return View();
            }
            else
            {
                Empresa E = new Empresa();
                E.RazonSocial = RazonSocialEmpresa_reg;
                E.Email = CorreoEmpresa_reg;
                E.RutRepresentante = RutRepresentanteEmpresa_reg;

                CreatePasswordHash(PasswordEmpresa_reg, out byte[] passwordHash, out byte[] passwordSalt);

                E.PasswordHash = passwordHash;
                E.PasswordSalt = passwordSalt;
                _context.Empresa.Add(E);
                _context.SaveChanges();
                return RedirectToAction("LoginIn", "Auth");
            }

        }



        [HttpPost]
        public async Task<IActionResult> EditarPasswordTrabajador(int id, string actualPassword, string newPassword)
        {

            var u = _context.Trabajador.FirstOrDefault(u => u.idTrabajador.Equals(id));
            if (u == null) { return NotFound(); };
            if (VerificarPass(actualPassword, u.PasswordHash, u.PasswordSalt))
            {
                CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

                u.PasswordHash = passwordHash;
                u.PasswordSalt = passwordSalt;
                _context.Update(u);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Trabajador");

            }
            else
            {
                return RedirectToAction("EditPassword", "Trabajador");
            }


        }
        

        [HttpPost]
        public async Task<IActionResult> EditarPasswordEmpresa(int id, string actualPassword, string newPassword)
        {

            var u = _context.Empresa.FirstOrDefault(u => u.idEmpresa.Equals(id));
            if (u == null) { return NotFound(); };
            if (VerificarPass(actualPassword, u.PasswordHash, u.PasswordSalt))
            {
                CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

                u.PasswordHash = passwordHash;
                u.PasswordSalt = passwordSalt;
                _context.Update(u);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Empresa");

            }
            else
            {
                return RedirectToAction("EditPassword","Empresa");
            }


        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //administrador 123456
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerificarPass(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var pass = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return pass.SequenceEqual(passwordHash);
            }
        }


    }
}