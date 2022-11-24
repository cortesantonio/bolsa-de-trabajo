using bolsadetrabajo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace bolsadetrabajo.Controllers
{
    public class PublicacionController : Controller
    {
        private readonly AppDbContext _context;
        public PublicacionController(AppDbContext context)
        {
            _context = context;
        }
        // GET: PublicacionController
        public ActionResult Index(int Id)
        {
            var P = _context.Publicacion.Find(Id);
            if (P == null)
            {
                return NotFound();
            }
            else
            {
                var publi = _context.Empresa
               .Include(p => p.idEmpresa)
               .FirstOrDefault();

                return View(publi);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPublicacion(Publicacion P)
        {
            if (ModelState.IsValid)
            {
                _context.Publicacion.Add(P);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Empresa");
            }
            else
            {
                ModelState.AddModelError("", "Ha Ocurrido un Error!");
                return View(P);
            }
        }

    }
}
