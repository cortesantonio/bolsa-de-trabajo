using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bolsadetrabajo.Models
{
    public class LoginViewModel : Controller
    {
        // GET: LoginViewModel
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoginViewModel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginViewModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginViewModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginViewModel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginViewModel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginViewModel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginViewModel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
