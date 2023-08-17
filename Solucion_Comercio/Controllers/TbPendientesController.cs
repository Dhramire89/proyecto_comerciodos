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



        // GET: TbPendientes/  Aprobar *******************************************************************************
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Si no se proporciona un ID válido, retornar "No encontrado"
            }

            
            var compra = await _context.TbCompras.FirstOrDefaultAsync(c => c.IdCompra == id.Value);
            
            if (compra == null)
            {
                return NotFound(); // Si la compra no se encuentra, retornar "No encontrado"
            }

            using var transaction = _context.Database.BeginTransaction(); // Iniciar transacción de base de datos

            try
            {
                var idProducto = compra.IdProducto; // ID del producto en la compra
                var cantidadCompra = compra.CantidadCompra; // Cantidad del producto en la compra

                var productoEnInventario = await _context.TbInventarios.FirstOrDefaultAsync(i => i.IdProducto == idProducto);

                if (productoEnInventario != null)
                {
                    productoEnInventario.Existencia += cantidadCompra; // Si el producto existe en el inventario, aumentar la cantidad en inventario
                }
                else
                {
                    _context.TbInventarios.Add(new TbInventario
                    {
                        IdProducto = idProducto, // Si el producto no existe en el inventario, agregarlo al inventario
                        Existencia = cantidadCompra
                    });
                }

                _context.TbCompras.Remove(compra); // Eliminar el registro de compra de la base de datos
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
                transaction.Commit(); // Confirmar la transacción

                return RedirectToAction(nameof(Index)); // Redirigir a la página principal u otra acción después de completar la transacción
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // Revertir la transacción en caso de error
                return BadRequest($"Error al aprobar la compra: {ex.Message}"); // Retornar un mensaje de error
            }
        }

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
            try
            {
                var tbPendiente = await _context.TbPendientes
                    .Include(t => t.IdCompraNavigation)
                    .FirstOrDefaultAsync(m => m.IdPendiente == id);
                if (tbPendiente == null)
                {
                    return NotFound();
                }

                return View(tbPendiente);
            }
            catch (Exception ex)
            {
                
                return BadRequest($"Error al aprobar la compra: {ex.Message}"); // Retornar un mensaje de error
            }
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
