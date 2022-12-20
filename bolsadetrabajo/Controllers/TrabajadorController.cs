using bolsadetrabajo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace bolsadetrabajo.Controllers
{
    [Authorize(Roles = "Trabajador")]
    public class TrabajadorController : Controller
    {

        private readonly AppDbContext _context;
        public TrabajadorController(AppDbContext context)
        {
            _context = context;
        }
        // GET: Trabajador
        public ActionResult Index()
        {
            var trabajador = _context.Trabajador.Where(c => c.Email.Equals(User.Identity.Name)).FirstOrDefault();
            return View(trabajador);
        }

        public ActionResult TrabajadorEdit()
        {
            var trabajador = _context.Trabajador.Where(c => c.Email.Equals(User.Identity.Name)).FirstOrDefault();
            if (trabajador == null)
            {
                return NotFound();
            }
            else
            {
                return View(trabajador);
            }
        }

        [HttpPost]
        public async Task<IActionResult> TrabajadorEditar(Trabajador T)
        {

            var u = _context.Trabajador.FirstOrDefault(u => u.idTrabajador.Equals(T.idTrabajador));
            if (u == null) { return NotFound(); };

            u.Nombre = T.Nombre;

            u.Email = T.Email;
            u.FechaNacimiento = T.FechaNacimiento;
            u.NumeroContacto = T.NumeroContacto;
            u.Sexo = T.Sexo;
            u.Profesion = T.Profesion;
            u.Direccion = T.Direccion;

            _context.Update(u);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        public ActionResult EditPassword()
        {
            var t = _context.Trabajador.Where(c => c.Email.Equals(User.Identity.Name)).FirstOrDefault();
            ViewBag.nombre = t.Nombre;
            ViewBag.id = t.idTrabajador;

            return View();
        }


        public ActionResult Favoritas()
        {
            var trabajador = _context.Trabajador
                .Where(x => x.Email.Equals(User.Identity.Name))
                .FirstOrDefault();

            var id = trabajador.idTrabajador;
            ViewBag.Nombre = trabajador.Nombre;

            return View(_context.PublicacionGuardada.Include(p => p.Publicacion.Empresa )
                .Where(x => x.TrabajadorId.Equals(id)).ToList() );
        }


    }
}
