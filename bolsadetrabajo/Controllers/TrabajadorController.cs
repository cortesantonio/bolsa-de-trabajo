using bolsadetrabajo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

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
        public async Task<IActionResult> TrabajadorEditar(TrabajadorViewModel Tvm)
        {
            if (ModelState.IsValid)
            {
                Trabajador T = new Trabajador();
                T.idTrabajador = Tvm.idTrabajador;
                T.Nombre = Tvm.Nombre;
                T.FechaNacimiento = Tvm.FechaNacimiento;
                T.NumeroContacto = Tvm.NumeroContacto;
                T.Direccion = Tvm.Direccion;
                T.Profesion = Tvm.Profesion;
                T.Rut = Tvm.Rut;
                T.Sexo = Tvm.Sexo;
                T.Email = Tvm.Email;
                


                _context.Trabajador.Update(T);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Ha Ocurrido un Error!");
                return View(Tvm);
            }
        }

    }
}
