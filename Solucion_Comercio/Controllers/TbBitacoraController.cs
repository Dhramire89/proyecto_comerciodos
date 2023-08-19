using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Solucion_Comercio.Models;

namespace Solucion_Comercio.Controllers
{
    public class TbBitacoraController : Controller

    {

        // GET: TbBitacoraController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TbBitacoraController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TbBitacoraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TbBitacoraController/Create
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

        // GET: TbBitacoraController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TbBitacoraController/Edit/5
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

        // GET: TbBitacoraController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TbBitacoraController/Delete/5
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
