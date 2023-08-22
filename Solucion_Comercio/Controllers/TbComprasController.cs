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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompra,NombreUsuario,FechaCompra,IdProducto,CantidadCompra")] TbCompra tbCompra)
        {
            if (ModelState.IsValid==false)
            {
                try
                {
                    // Agregar el registro de compra en la tabla TbCompras
                    _context.TbCompras.Add(tbCompra);
                    await _context.SaveChangesAsync();

                    // Agregar un registro en la tabla TbPendientes con el valor de IdCompra

                    try
                    {
                        // Obtener el último IdPendiente
            int ultimoIdPendiente = await _context.TbPendientes
                .OrderByDescending(p => p.IdPendiente)
                .Select(p => p.IdPendiente)
                .FirstOrDefaultAsync();

            // Incrementar el último IdPendiente para obtener el nuevo valor
            int nuevoIdPendiente = ultimoIdPendiente + 1;

            // Agregar un registro en la tabla TbPendientes con el valor de IdCompra
            TbPendiente nuevopendiente = new TbPendiente
            {
                IdPendiente = nuevoIdPendiente,
                            IdCompra = tbCompra.IdCompra
                        };
                        _context.TbPendientes.Add(nuevopendiente);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException ex)
                    {
                        var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
                        return BadRequest($"Error al guardar los cambios: {innerExceptionMessage}");
                    }

                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al crear la compra o pendiente: {ex.Message}");
                }
            }

            // Si el modelo no es válido o hubo un error, configurar el SelectList para IdProducto
            ViewData["IdProducto"] = new SelectList(_context.TbProductos, "IdProducto", "NombreProducto", tbCompra.IdProducto);
            return View(tbCompra);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("IdCompra,NombreUsuario,FechaCompra,IdProducto,CantidadCompra")] TbCompra tbCompra)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid == false)
        //        {
        //            // Agregar el registro de compra en la tabla TbCompras
        //            _context.TbCompras.Add(tbCompra);
        //            await _context.SaveChangesAsync();
        //            try
        //            {
        //                // Agregar un registro en la tabla TbPendientes con el valor de IdCompra
        //                var valor = 0;
        //                TbPendiente nuevopendiente = new TbPendiente();


        //                valor = tbCompra.IdCompra; 
        //                nuevopendiente.IdCompra = valor;

        //                _context.TbPendientes.Add(nuevopendiente);
        //                await _context.SaveChangesAsync();



        //                return RedirectToAction(nameof(Index));
        //            }
        //            catch (Exception ex)
        //            {

        //                return BadRequest($"Error al crear pendiente: {ex.Message}"); // Retornar un mensaje de error
        //            }
        //        }
        //        else
        //        {
        //            // Si el modelo no es válido, configurar el SelectList para IdProducto
        //            ViewData["IdProducto"] = new SelectList(_context.TbProductos, "IdProducto", "NombreProducto", tbCompra.IdProducto);
        //            return View(tbCompra);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar la excepción adecuadamente (puedes registrarla, mostrar un mensaje de error, etc.)
        //        ModelState.AddModelError("", $"Error al crear la compra: {ex.Message}");

        //        // Configurar el SelectList para IdProducto nuevamente
        //        ViewData["IdProducto"] = new SelectList(_context.TbProductos, "IdProducto", "NombreProducto", tbCompra.IdProducto);

        //        return View(tbCompra);
        //    }
        //}


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
