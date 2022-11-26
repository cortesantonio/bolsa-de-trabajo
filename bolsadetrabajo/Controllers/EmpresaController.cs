using bolsadetrabajo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace bolsadetrabajo.Controllers
{
    [Authorize(Roles = "Empresa")]

    public class EmpresaController : Controller
    {
        private readonly AppDbContext _context;
        public EmpresaController(AppDbContext context)
        {
            _context = context;
        }
        // GET: EmpresaController
        public ActionResult Index()
        {
           var empresa = _context.Empresa.Where(c => c.Email.Equals(User.Identity.Name)).FirstOrDefault();
            return View(empresa);
        }

        public ActionResult Create()
        {
            var empresa = _context.Empresa.Where(c => c.Email.Equals(User.Identity.Name)).FirstOrDefault();
            ViewBag.empresaid = empresa.idEmpresa;
            ViewBag.empresaNombre = empresa.RazonSocial;
            ViewBag.fecha = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss");

            return View();
        }
        public IActionResult ViewPublicaciones()
        {
            var empresa = _context.Empresa.Where(c => c.Email.Equals(User.Identity.Name)).FirstOrDefault();
            var publicaciones = _context.Publicacion.Where(c => c.EmpresaId ==empresa.idEmpresa ).ToList();
            ViewBag.razonsocial = empresa.RazonSocial;

            return View(publicaciones);
        }
        public ActionResult EditPublicacion(int Id)
        {
            var publicacion = _context.Publicacion.Where(c => c.idPublicacion.Equals(Id)).FirstOrDefault();
            if (publicacion == null)
            {
                return NotFound();
            }
            else
            {
                var empresa = _context.Empresa.Where(c => c.Email.Equals(User.Identity.Name)).FirstOrDefault();
                ViewBag.empresaid = empresa.idEmpresa;
                ViewBag.empresaNombre = empresa.RazonSocial;
                ViewBag.fecha = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss");
                return View(publicacion);
            }
        }

        public ActionResult EmpresaEdit()
        {
            var E = _context.Empresa.Where(c => c.Email.Equals(User.Identity.Name)).FirstOrDefault();
            if (E == null)
            {
                return NotFound();
            }
            else
            {
                return View(E);
            }
        }
        public ActionResult EditPassword()
        {
            var t = _context.Empresa.Where(c => c.Email.Equals(User.Identity.Name)).FirstOrDefault();
            ViewBag.nombre = t.RazonSocial;
            ViewBag.id = t.idEmpresa;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EmpresaEditar(Empresa E)
        {

            var u = _context.Empresa.FirstOrDefault(u => u.idEmpresa.Equals(E.idEmpresa));
            if (u == null) { return NotFound(); };

            u.RazonSocial = E.RazonSocial;

            u.Email = E.Email;
            u.NombreRepresentante = E.NombreRepresentante;
            u.RutRepresentante = E.RutRepresentante;
            u.Rut = E.Rut;
            u.NumeroContacto = E.NumeroContacto;
            u.Direccion = E.Direccion;

            _context.Update(u);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
