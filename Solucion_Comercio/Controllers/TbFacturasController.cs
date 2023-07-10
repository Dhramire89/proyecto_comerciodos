using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Solucion_Comercio.Models;

namespace Solucion_Comercio.Controllers
{
    [Authorize(Policy =  "PoliticaCajero")]
    public class TbFacturasController : Controller
    {
        private readonly BdcomercioContext _context;

        public TbFacturasController(BdcomercioContext context)
        {
            _context = context;
        }

        // GET: TbFacturas
        public async Task<IActionResult> Index()
        {
            var bdcomercioContext = _context.TbFacturas.Include(t => t.NombreUsuarioNavigation);
            return View(await bdcomercioContext.ToListAsync());
        }

        // GET: TbFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbFacturas == null)
            {
                return NotFound();
            }

            var tbFactura = await _context.TbFacturas
                .Include(t => t.NombreUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (tbFactura == null)
            {
                return NotFound();
            }

            return View(tbFactura);
        }

        // GET: TbFacturas/Create
        public IActionResult Create()
        {
            ViewData["NombreUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: TbFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFactura,NombreCliente,NombreUsuario,FechaFactura,MontoColones,MontoDolares,MontoTarjeta,MontoTotal")] TbFactura tbFactura)
        {
            if (ModelState.IsValid == false)
            {
                _context.Add(tbFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NombreUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbFactura.NombreUsuario);
            return View(tbFactura);
        }

        // GET: TbFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbFacturas == null)
            {
                return NotFound();
            }

            var tbFactura = await _context.TbFacturas.FindAsync(id);
            if (tbFactura == null)
            {
                return NotFound();
            }
            ViewData["NombreUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbFactura.NombreUsuario);
            return View(tbFactura);
        }

        // POST: TbFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFactura,NombreCliente,NombreUsuario,FechaFactura,MontoColones,MontoDolares,MontoTarjeta,MontoTotal")] TbFactura tbFactura)
        {
            if (id != tbFactura.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbFacturaExists(tbFactura.IdFactura))
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
            ViewData["NombreUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbFactura.NombreUsuario);
            return View(tbFactura);
        }

        // GET: TbFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbFacturas == null)
            {
                return NotFound();
            }

            var tbFactura = await _context.TbFacturas
                .Include(t => t.NombreUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (tbFactura == null)
            {
                return NotFound();
            }

            return View(tbFactura);
        }

        // POST: TbFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbFacturas == null)
            {
                return Problem("Entity set 'BdcomercioContext.TbFacturas'  is null.");
            }
            var tbFactura = await _context.TbFacturas.FindAsync(id);
            if (tbFactura != null)
            {
                _context.TbFacturas.Remove(tbFactura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        private bool TbFacturaExists(int id)
        {
          return (_context.TbFacturas?.Any(e => e.IdFactura == id)).GetValueOrDefault();
        }
    }
}
