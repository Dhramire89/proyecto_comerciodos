using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Solucion_Comercio.Models;
using Solucion_Comercio.Servicios.Contrato;

namespace Solucion_Comercio.Controllers
{
    public class TbBitacoraController : Controller

    {

        private readonly BdcomercioContext _context;

        public TbBitacoraController(BdcomercioContext context)
        {
            _context = context;
        }

        // GET: TbBitacoraController
        public async Task<IActionResult> Index()
        {
            return _context.TbRoles != null ?
                          View(await _context.TbBitacoras.ToListAsync()) :
                          Problem("Entity set 'BdcomercioContext.TbBitacoras'  is null.");
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
