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
    public class TbInventariosController : Controller
    {
        private readonly BdcomercioContext _context;

        public TbInventariosController(BdcomercioContext context)
        {
            _context = context;
        }

        // GET: TbInventarios
        public async Task<IActionResult> Index()
        {
            var bdcomercioContext = _context.TbInventarios.Include(t => t.IdProductoNavigation);
            return View(await bdcomercioContext.ToListAsync());
        }

        // GET: TbInventarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbInventarios == null)
            {
                return NotFound();
            }

            var tbInventario = await _context.TbInventarios
                .Include(t => t.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdInventario == id);
            if (tbInventario == null)
            {
                return NotFound();
            }

            return View(tbInventario);
        }

        // GET: TbInventarios/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.TbProductos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: TbInventarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInventario,IdProducto,Existencia")] TbInventario tbInventario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbInventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.TbProductos, "IdProducto", "IdProducto", tbInventario.IdProducto);
            return View(tbInventario);
        }

        // GET: TbInventarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbInventarios == null)
            {
                return NotFound();
            }

            var tbInventario = await _context.TbInventarios.FindAsync(id);
            if (tbInventario == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.TbProductos, "IdProducto", "IdProducto", tbInventario.IdProducto);
            return View(tbInventario);
        }

        // POST: TbInventarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInventario,IdProducto,Existencia")] TbInventario tbInventario)
        {
            if (id != tbInventario.IdInventario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbInventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbInventarioExists(tbInventario.IdInventario))
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
            ViewData["IdProducto"] = new SelectList(_context.TbProductos, "IdProducto", "IdProducto", tbInventario.IdProducto);
            return View(tbInventario);
        }

        // GET: TbInventarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbInventarios == null)
            {
                return NotFound();
            }

            var tbInventario = await _context.TbInventarios
                .Include(t => t.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdInventario == id);
            if (tbInventario == null)
            {
                return NotFound();
            }

            return View(tbInventario);
        }

        // POST: TbInventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbInventarios == null)
            {
                return Problem("Entity set 'BdcomercioContext.TbInventarios'  is null.");
            }
            var tbInventario = await _context.TbInventarios.FindAsync(id);
            if (tbInventario != null)
            {
                _context.TbInventarios.Remove(tbInventario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        private bool TbInventarioExists(int id)
        {
          return (_context.TbInventarios?.Any(e => e.IdInventario == id)).GetValueOrDefault();
        }
    }
}
