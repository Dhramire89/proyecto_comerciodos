using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Solucion_Comercio.Models;

namespace Solucion_Comercio.Controllers
{

    [Authorize(Policy = "PoliticaCocinero")]
    public class TbPendientesController : Controller
    {
        private readonly BdcomercioContext _context;

        public TbPendientesController(BdcomercioContext context)
        {
            _context = context;
        }

        // GET: TbPendientes
        public async Task<IActionResult> Index()
        {
            var bdcomercioContext = _context.TbPendientes.Include(t => t.IdCompraNavigation);
            return View(await bdcomercioContext.ToListAsync());
        }

        // GET: TbPendientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbPendientes == null)
            {
                return NotFound();
            }

            var tbPendiente = await _context.TbPendientes
                .Include(t => t.IdCompraNavigation)
                .FirstOrDefaultAsync(m => m.IdPendiente == id);
            if (tbPendiente == null)
            {
                return NotFound();
            }

            return View(tbPendiente);
        }

        // GET: TbPendientes/Create
        public IActionResult Create()
        {
            ViewData["IdCompra"] = new SelectList(_context.TbCompras, "IdCompra", "IdCompra");
            return View();
        }

        // POST: TbPendientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPendiente,IdCompra")] TbPendiente tbPendiente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbPendiente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompra"] = new SelectList(_context.TbCompras, "IdCompra", "IdCompra", tbPendiente.IdCompra);
            return View(tbPendiente);
        }

        // GET: TbPendientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbPendientes == null)
            {
                return NotFound();
            }

            var tbPendiente = await _context.TbPendientes.FindAsync(id);
            if (tbPendiente == null)
            {
                return NotFound();
            }
            ViewData["IdCompra"] = new SelectList(_context.TbCompras, "IdCompra", "IdCompra", tbPendiente.IdCompra);
            return View(tbPendiente);
        }

        // POST: TbPendientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPendiente,IdCompra")] TbPendiente tbPendiente)
        {
            if (id != tbPendiente.IdPendiente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbPendiente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbPendienteExists(tbPendiente.IdPendiente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompra"] = new SelectList(_context.TbCompras, "IdCompra", "IdCompra", tbPendiente.IdCompra);
            return View(tbPendiente);
        }

        // GET: TbPendientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbPendientes == null)
            {
                return NotFound();
            }

            var tbPendiente = await _context.TbPendientes
                .Include(t => t.IdCompraNavigation)
                .FirstOrDefaultAsync(m => m.IdPendiente == id);
            if (tbPendiente == null)
            {
                return NotFound();
            }

            return View(tbPendiente);
        }

        // POST: TbPendientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbPendientes == null)
            {
                return Problem("Entity set 'BdcomercioContext.TbPendientes'  is null.");
            }
            var tbPendiente = await _context.TbPendientes.FindAsync(id);
            if (tbPendiente != null)
            {
                _context.TbPendientes.Remove(tbPendiente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        private bool TbPendienteExists(int id)
        {
          return (_context.TbPendientes?.Any(e => e.IdPendiente == id)).GetValueOrDefault();
        }
    }
}
