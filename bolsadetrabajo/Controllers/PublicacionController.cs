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
        public ActionResult anuncio(int Id)
        {
            var P = _context.Publicacion.Find(Id);
            if (P == null)
            {
                return NotFound();
            }
            else
            {
                var publi = _context.Publicacion
               .Include(p => p.Empresa)
               .Where(x => x.idPublicacion == Id)
               .FirstOrDefault();

                if (User.IsInRole("Trabajador"))
                {
                    var us = _context.Trabajador.Where(x => x.Email.Equals(User.Identity.Name)).FirstOrDefault();
                    var trabajadorId = us.idTrabajador;

                    ViewBag.isFav = _context.PublicacionGuardada.Where(x => x.PublicacionId.Equals(Id) && x.TrabajadorId.Equals(trabajadorId)).FirstOrDefault();

                    var isView = _context.VisitasPublicacion.Where(x => x.PublicacionId.Equals(Id) && x.TrabajadorId.Equals(trabajadorId)).FirstOrDefault() ;

                    if (isView == null) {

                        VisitasPublicacion vp = new VisitasPublicacion();
                        vp.PublicacionId = Id;
                        vp.TrabajadorId = trabajadorId;
                        _context.VisitasPublicacion.Add(vp);
                        _context.SaveChanges();
                    }

                    
                }
                if (User.IsInRole("Empresa"))
                {

                    var isProp = _context.Publicacion
                        .Where(x => x.Empresa.Email.Equals(User.Identity.Name)
                        && x.idPublicacion.Equals(Id));
                    if (isProp !=null) {

                        ViewBag.views = _context.VisitasPublicacion.Where(x => x.PublicacionId.Equals(Id)).ToList().Count;
                        ViewBag.heart = _context.PublicacionGuardada.Where(x => x.PublicacionId.Equals(Id)).ToList().Count;
                        ViewBag.numcontact = _context.NumeroContactoPublicacions.Where(x => x.PublicacionId.Equals(Id)).ToList().Count;

                    }
                        
                }




                return View(publi);
            }
        }

        [HttpPost]
        public ActionResult Fav(int idAnuncio) {

            var us = _context.Trabajador.Where(x => x.Email.Equals(User.Identity.Name)).FirstOrDefault();
            var trabajadorId = us.idTrabajador;

            var pubGuardada = _context.PublicacionGuardada.Where(x => x.PublicacionId
            .Equals(idAnuncio) && x.TrabajadorId.Equals(trabajadorId)).FirstOrDefault();

            if (pubGuardada  != null) {

                _context.PublicacionGuardada.Remove(pubGuardada);
                _context.SaveChanges();

                return RedirectToAction("anuncio", new {Id = idAnuncio});

            }
            else
            {
                PublicacionGuardada pg = new PublicacionGuardada();
                pg.TrabajadorId = trabajadorId;
                pg.PublicacionId = idAnuncio;
                _context.PublicacionGuardada.Add(pg);
                _context.SaveChanges();
                return RedirectToAction("anuncio", new { Id = idAnuncio });

            }

        }

        [HttpPost]
        public ActionResult contact(int idAnuncio)
        {

            var us = _context.Trabajador.Where(x => x.Email.Equals(User.Identity.Name)).FirstOrDefault();
            var trabajadorId = us.idTrabajador;

            var contactoHecho = _context.NumeroContactoPublicacions.Where(x => x.PublicacionId
            .Equals(idAnuncio) && x.TrabajadorId.Equals(trabajadorId)).FirstOrDefault();

            if (contactoHecho != null)
            {
                var num = _context.Publicacion.Include(x => x.Empresa)
                    .Where(x => x.idPublicacion.Equals(idAnuncio)).FirstOrDefault();
                return Redirect($"tel:{num.Empresa.NumeroContacto}");

            }
            else
            {
                NumeroContactoPublicacion nc = new NumeroContactoPublicacion();
                nc.TrabajadorId = trabajadorId;
                nc.PublicacionId = idAnuncio;
                _context.NumeroContactoPublicacions.Add(nc);
                _context.SaveChanges();
                var num = _context.Publicacion.Where(x => x.idPublicacion.Equals(idAnuncio)).FirstOrDefault().Empresa.NumeroContacto;
                return Redirect($"tel:{num}");

            }

        }
        public ActionResult delFav (int idAnuncio)
        {

            var us = _context.Trabajador.Where(x => x.Email.Equals(User.Identity.Name)).FirstOrDefault();
            var trabajadorId = us.idTrabajador;

            var pubGuardada = _context.PublicacionGuardada.Where(x => x.PublicacionId.Equals(idAnuncio) && x.TrabajadorId.Equals(trabajadorId)).FirstOrDefault();

            

             _context.PublicacionGuardada.Remove(pubGuardada);
             _context.SaveChanges();

             return RedirectToAction("Favoritas" , "Trabajador");

        }



        public ActionResult catalogo(string busqueda)
        {
            if(busqueda !=null)
            {
                var publi = _context.Publicacion.Include(p => p.Empresa).Where(x => x.Titulo.Contains(busqueda) 
                || x.Area.Contains(busqueda) || x.Descripcion.Contains(busqueda) || x.Empresa.RazonSocial.Contains(busqueda) ).ToList();
                return View(publi);

            }
            else { 
            
                var publi = _context.Publicacion.Include(p => p.Empresa).ToList();
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
                return RedirectToAction("Create", "Empresa");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarPublicacion(Publicacion P)
        {
            if (ModelState.IsValid)
            {
                _context.Publicacion.Update(P);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Empresa");
            }
            else
            {
                ModelState.AddModelError("", "Ha Ocurrido un Error!");
                return RedirectToAction("EditPublicacion", "Empresa");
            }
        }
        public IActionResult DeletePublicacion(int Id)
        {
            var P = _context.Publicacion.Find(Id);
            if (P == null)
            {
                return NotFound();
            }
            else
            {
                _context.Publicacion.Remove(P);
                _context.SaveChanges();
                return RedirectToAction("ViewPublicaciones","Empresa");
            }
        }

    }
}
