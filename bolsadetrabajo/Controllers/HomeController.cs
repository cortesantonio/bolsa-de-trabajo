using bolsadetrabajo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace bolsadetrabajo.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        private readonly AppDbContext _context;

     

        public IActionResult Index()
        {
            var publi = _context.Publicacion
               .Include(p => p.Empresa).Take(3).OrderByDescending(p => p.Fecha).ToList();

            return View(publi);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}