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
            return View();
        }


    }
}
