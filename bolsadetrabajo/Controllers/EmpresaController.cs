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
            ViewBag.fecha = DateTime.UtcNow.ToString("dd-MM-yyyy");

            return View();
        }
        public IActionResult ViewPublicaciones()
        {
            var empresa = _context.Empresa.Where(c => c.Email.Equals(User.Identity.Name)).FirstOrDefault();
            var publicaciones = _context.Publicacion.Where(c => c.EmpresaId ==empresa.idEmpresa ).ToList();
            ViewBag.razonsocial = empresa.RazonSocial;

            return View(publicaciones);
        }


    }
}
