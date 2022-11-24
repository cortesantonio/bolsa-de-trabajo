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

            return View();
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
                        new Claim(ClaimTypes.Name, us.Nombre),
                        new Claim(ClaimTypes.NameIdentifier, CorreoTrabajador),
                        new Claim(ClaimTypes.Role, "Trabajador")
                    };

                    //Carnet, Licencia
                    var identity = new ClaimsIdentity(Claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties { IsPersistent = true }
                        );

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Usuario correcto pero contraseña mala
                    ModelState.AddModelError("", "Contraseña Incorrecta");
                    return View();
                }


            }
            else
            {
                //Usuario No Existe
                ModelState.AddModelError("", "Usuario no Encontrado!");
                return View();
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











        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //administrador 123456
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerificarPass(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var pass = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return pass.SequenceEqual(passwordHash);
            }
        }


    }
}