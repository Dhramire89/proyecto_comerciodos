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
    public class TbRolesController : Controller
    {
        private readonly BdcomercioContext _context;

        public TbRolesController(BdcomercioContext context)
        {
            _context = context;
        }

        // GET: TbRoles
        public async Task<IActionResult> Index()
        {
              return _context.TbRoles != null ? 
                          View(await _context.TbRoles.ToListAsync()) :
                          Problem("Entity set 'BdcomercioContext.TbRoles'  is null.");
        }

        // GET: TbRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbRoles == null)
            {
                return NotFound();
            }

            var tbRole = await _context.TbRoles
                .FirstOrDefaultAsync(m => m.IdRol == id);
            if (tbRole == null)
            {
                return NotFound();
            }

            return View(tbRole);
        }

        // GET: TbRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRol,NombreRol")] TbRole tbRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbRole);
        }

        // GET: TbRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbRoles == null)
            {
                return NotFound();
            }

            var tbRole = await _context.TbRoles.FindAsync(id);
            if (tbRole == null)
            {
                return NotFound();
            }
            return View(tbRole);
        }

        // POST: TbRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRol,NombreRol")] TbRole tbRole)
        {
            if (id != tbRole.IdRol)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbRoleExists(tbRole.IdRol))
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
            return View(tbRole);
        }

        // GET: TbRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbRoles == null)
            {
                return NotFound();
            }

            var tbRole = await _context.TbRoles
                .FirstOrDefaultAsync(m => m.IdRol == id);
            if (tbRole == null)
            {
                return NotFound();
            }

            return View(tbRole);
        }

        // POST: TbRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbRoles == null)
            {
                return Problem("Entity set 'BdcomercioContext.TbRoles'  is null.");
            }
            var tbRole = await _context.TbRoles.FindAsync(id);
            if (tbRole != null)
            {
                _context.TbRoles.Remove(tbRole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        private bool TbRoleExists(int id)
        {
          return (_context.TbRoles?.Any(e => e.IdRol == id)).GetValueOrDefault();
        }
    }
}
