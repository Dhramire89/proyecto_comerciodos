using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Solucion_Comercio.Models;


namespace Solucion_Comercio.Controllers
{


    [Authorize(Policy = "PoliticaAdiministrador")]

    public class TbUsuariosController : Controller
    {
        private readonly BdcomercioContext _context;

        public TbUsuariosController(BdcomercioContext context)
        {
            _context = context;
        }

        // GET: TbUsuarios
        public async Task<IActionResult> Index()
        {
            var bdcomercioContext = _context.TbUsuarios.Include(t => t.EstadoUsuarioNavigation).Include(t => t.RolUsuarioNavigation);
            return View(await bdcomercioContext.ToListAsync());
        }

        // GET: TbUsuarios/Details/5

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbUsuarios == null)
            {
                return NotFound();
            }

            var tbUsuario = await _context.TbUsuarios
                .Include(t => t.EstadoUsuarioNavigation)
                .Include(t => t.RolUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tbUsuario == null)
            {
                return NotFound();
            }

            return View(tbUsuario);
        }

        // GET: TbUsuarios/Create
      
        public IActionResult Create()
        {
            ViewData["EstadoUsuario"] = new SelectList(_context.TbEstados, "IdEstado", "NombreEstado");
            ViewData["RolUsuario"] = new SelectList(_context.TbRoles, "IdRol", "NombreRol");
            return View();
        }

        // POST: TbUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,NombreUsuario,ApellidoUsuario,ApellidoIiusuario,UserName,Password,RolUsuario,EstadoUsuario,CorreoUsuario,TelefonoUsuario")] TbUsuario tbUsuario)
        {
            if (ModelState.IsValid==false)
            {
                _context.Add(tbUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoUsuario"] = new SelectList(_context.TbEstados, "IdEstado", "NombreEstado", tbUsuario.EstadoUsuario);
            ViewData["RolUsuario"] = new SelectList(_context.TbRoles, "IdRol", "NombreRol", tbUsuario.RolUsuario);
            return View(tbUsuario);
        }

        // GET: TbUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbUsuarios == null)
            {
                return NotFound();
            }

            var tbUsuario = await _context.TbUsuarios.FindAsync(id);
            if (tbUsuario == null)
            {
                return NotFound();
            }
            ViewData["EstadoUsuario"] = new SelectList(_context.TbEstados, "IdEstado", "NombreEstado", tbUsuario.EstadoUsuario);
            ViewData["RolUsuario"] = new SelectList(_context.TbRoles, "IdRol", "NombreRol", tbUsuario.RolUsuario);
            return View(tbUsuario);
        }

        // POST: TbUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,NombreUsuario,ApellidoUsuario,ApellidoIiusuario,UserName,Password,RolUsuario,EstadoUsuario,CorreoUsuario,TelefonoUsuario")] TbUsuario tbUsuario)
        {
            if (id != tbUsuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid==false)
            {
                try
                {
                    _context.Update(tbUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbUsuarioExists(tbUsuario.IdUsuario))
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
            ViewData["EstadoUsuario"] = new SelectList(_context.TbEstados, "IdEstado", "NombreEstado", tbUsuario.EstadoUsuario);
            ViewData["RolUsuario"] = new SelectList(_context.TbRoles, "IdRol", "NombreRol", tbUsuario.RolUsuario);
            return View(tbUsuario);
        }

        // GET: TbUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbUsuarios == null)
            {
                return NotFound();
            }

            var tbUsuario = await _context.TbUsuarios
                .Include(t => t.EstadoUsuarioNavigation)
                .Include(t => t.RolUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tbUsuario == null)
            {
                return NotFound();
            }

            return View(tbUsuario);
        }

        // POST: TbUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbUsuarios == null)
            {
                return Problem("Entity set 'BdcomercioContext.TbUsuarios'  is null.");
            }
            var tbUsuario = await _context.TbUsuarios.FindAsync(id);
            if (tbUsuario != null)
            {
                _context.TbUsuarios.Remove(tbUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        private bool TbUsuarioExists(int id)
        {
          return (_context.TbUsuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }



    }
}
