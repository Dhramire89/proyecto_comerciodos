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
    [Authorize(Policy = "PoliticaAdiministrador")]
    public class TbProductoesController : Controller
    {
        private readonly BdcomercioContext _context;

        public TbProductoesController(BdcomercioContext context)
        {
            _context = context;
        }
        //cu
        // GET: TbProductoes
        public async Task<IActionResult> Index()
        {
              return _context.TbProductos != null ? 
                          View(await _context.TbProductos.ToListAsync()) :
                          Problem("Entity set 'BdcomercioContext.TbProductos'  is null.");
        }

        // GET: TbProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProductos == null)
            {
                return NotFound();
            }

            var tbProducto = await _context.TbProductos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (tbProducto == null)
            {
                return NotFound();
            }

            return View(tbProducto);
        }

        // GET: TbProductoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,NombreProducto,CantidadProducto,PrecioProducto")] TbProducto tbProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbProducto);
        }

        // GET: TbProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbProductos == null)
            {
                return NotFound();
            }

            var tbProducto = await _context.TbProductos.FindAsync(id);
            if (tbProducto == null)
            {
                return NotFound();
            }
            return View(tbProducto);
        }

        // POST: TbProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,NombreProducto,CantidadProducto,PrecioProducto")] TbProducto tbProducto)
        {
            if (id != tbProducto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProductoExists(tbProducto.IdProducto))
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
            return View(tbProducto);
        }

        // GET: TbProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbProductos == null)
            {
                return NotFound();
            }

            var tbProducto = await _context.TbProductos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (tbProducto == null)
            {
                return NotFound();
            }

            return View(tbProducto);
        }

        // POST: TbProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbProductos == null)
            {
                return Problem("Entity set 'BdcomercioContext.TbProductos'  is null.");
            }
            var tbProducto = await _context.TbProductos.FindAsync(id);
            if (tbProducto != null)
            {
                _context.TbProductos.Remove(tbProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        private bool TbProductoExists(int id)
        {
          return (_context.TbProductos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}
