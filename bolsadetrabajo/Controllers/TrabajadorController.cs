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
        public IActionResult TrabajadorEditar(Trabajador T)
        {

            if (ModelState.IsValid)
            {

                _context.Trabajador.Update(T);
                _context.SaveChanges();
                
                return RedirectToAction("Index", "Trabajador"); 
            }

            else
            {
                ModelState.AddModelError("", "Ha Ocurrido un Error!");
                return RedirectToAction("TrabajadorEdit", "Trabajador");
            }
            
        }

    }
}
