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
    public class TbEstadoesController : Controller
    {
        private readonly BdcomercioContext _context;

        public TbEstadoesController(BdcomercioContext context)
        {
            _context = context;
        }

        // GET: TbEstadoes
        public async Task<IActionResult> Index()
        {
              return _context.TbEstados != null ? 
                          View(await _context.TbEstados.ToListAsync()) :
                          Problem("Entity set 'BdcomercioContext.TbEstados'  is null.");
        }

        // GET: TbEstadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbEstados == null)
            {
                return NotFound();
            }

            var tbEstado = await _context.TbEstados
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (tbEstado == null)
            {
                return NotFound();
            }

            return View(tbEstado);
        }

        // GET: TbEstadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbEstadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstado,NombreEstado")] TbEstado tbEstado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbEstado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbEstado);
        }

        // GET: TbEstadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbEstados == null)
            {
                return NotFound();
            }

            var tbEstado = await _context.TbEstados.FindAsync(id);
            if (tbEstado == null)
            {
                return NotFound();
            }
            return View(tbEstado);
        }

        // POST: TbEstadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IdEstado,NombreEstado")] TbEstado tbEstado)
        //{
        //    if (id != tbEstado.IdEstado)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(tbEstado);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TbEstadoExists(tbEstado.IdEstado))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(tbEstado);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstado,NombreEstado")] TbEstado tbEstado)
        {
            if (id != tbEstado.IdEstado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el estado del usuario
                    var usuario = await _context.TbUsuarios.FindAsync(id);
                    if (usuario != null)
                    {
                        usuario.EstadoUsuario = tbEstado.IdEstado;
                        _context.Update(usuario);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbEstadoExists(tbEstado.IdEstado))
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
            return View(tbEstado);
        }



        // GET: TbEstadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbEstados == null)
            {
                return NotFound();
            }

            var tbEstado = await _context.TbEstados
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (tbEstado == null)
            {
                return NotFound();
            }

            return View(tbEstado);
        }

        // POST: TbEstadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbEstados == null)
            {
                return Problem("Entity set 'BdcomercioContext.TbEstados'  is null.");
            }
            var tbEstado = await _context.TbEstados.FindAsync(id);
            if (tbEstado != null)
            {
                _context.TbEstados.Remove(tbEstado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        private bool TbEstadoExists(int id)
        {
          return (_context.TbEstados?.Any(e => e.IdEstado == id)).GetValueOrDefault();
        }
    }
}
