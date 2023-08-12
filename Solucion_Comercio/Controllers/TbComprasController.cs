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
    public class TbComprasController : Controller
    {
        private readonly BdcomercioContext _context;

        public TbComprasController(BdcomercioContext context)
        {
            _context = context;
        }

        // GET: TbCompras
        public async Task<IActionResult> Index()
        {
            var bdcomercioContext = _context.TbCompras.Include(t => t.IdProductoNavigation);
            return View(await bdcomercioContext.ToListAsync());
        }

        // GET: TbCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbCompras == null)
            {
                return NotFound();
            }

            var tbCompra = await _context.TbCompras
                .Include(t => t.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (tbCompra == null)
            {
                return NotFound();
            }

            return View(tbCompra);
        }

        // GET: TbCompras/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.TbProductos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: TbCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompra,NombreUsuario,FechaCompra,IdProducto,CantidadCompra")] TbCompra tbCompra)
        {
            if (ModelState.IsValid==false)
            {
                _context.Add(tbCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.TbProductos, "IdProducto", "IdProducto", tbCompra.IdProducto);
            return View(tbCompra);
        }

        // GET: TbCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbCompras == null)
            {
                return NotFound();
            }

            var tbCompra = await _context.TbCompras.FindAsync(id);
            if (tbCompra == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.TbProductos, "IdProducto", "IdProducto", tbCompra.IdProducto);
            return View(tbCompra);
        }

        // POST: TbCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompra,NombreUsuario,FechaCompra,IdProducto,CantidadCompra")] TbCompra tbCompra)
        {
            if (id != tbCompra.IdCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCompraExists(tbCompra.IdCompra))
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
            ViewData["IdProducto"] = new SelectList(_context.TbProductos, "IdProducto", "IdProducto", tbCompra.IdProducto);
            return View(tbCompra);
        }

        // GET: TbCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbCompras == null)
            {
                return NotFound();
            }

            var tbCompra = await _context.TbCompras
                .Include(t => t.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (tbCompra == null)
            {
                return NotFound();
            }

            return View(tbCompra);
        }

        // POST: TbCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbCompras == null)
            {
                return Problem("Entity set 'BdcomercioContext.TbCompras'  is null.");
            }
            var tbCompra = await _context.TbCompras.FindAsync(id);
            if (tbCompra != null)
            {
                _context.TbCompras.Remove(tbCompra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        private bool TbCompraExists(int id)
        {
          return (_context.TbCompras?.Any(e => e.IdCompra == id)).GetValueOrDefault();
        }
    }
}
