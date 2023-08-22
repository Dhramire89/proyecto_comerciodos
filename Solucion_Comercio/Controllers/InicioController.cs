using Microsoft.AspNetCore.Mvc;
using Solucion_Comercio.Models;
using Solucion_Comercio.Servicios.Contrato;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Solucion_Comercio.Controllers
{
    public class InicioController : Controller
    {

        //private readonly BdcomercioContext _context;

        private readonly IUsuarioService _usuarioServicio;
        private readonly BdcomercioContext _context;
        public InicioController(IUsuarioService usuarioServicio, BdcomercioContext context)
        {
            _usuarioServicio = usuarioServicio;
            _context = context;
        }

        public IActionResult IniciarSeccion()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> IniciarSeccion(string correo, string clave)
        {
            TbUsuario usuario_encontrado = _usuarioServicio.GetUsuario(correo);

            if (usuario_encontrado != null)
            {
                try //ad
                {
                    TempData["UsuarioId"] = usuario_encontrado.IdUsuario;

                    if (usuario_encontrado.Password == clave && usuario_encontrado.EstadoUsuario == 1)
                    {
                        // Obtener el id del rol del usuario
                        int idRol = usuario_encontrado.RolUsuario;

                        //// Crear una nueva entrada en la tabla TbBitacota
                        TbBitacora nuevaEntrada = new TbBitacora
                        {
                            IdUsuario = usuario_encontrado.IdUsuario, // ID del usuario
                            Entrada = DateTime.Now, // Fecha y hora actual
                            Salida = DateTime.Now,
                        };

                        try
                        {
                            // Agregar la nueva entrada a la tabla bitacora
                            _context.TbBitacoras.Add(nuevaEntrada);
                            _context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return BadRequest($"Error al crear bitacoara: {ex.Message}"); // Retornar un mensaje de error
                        }

                        // claims para la seguridad de cada usuario
                        List<Claim> claims = new List<Claim>(){
        new Claim(ClaimTypes.Name, usuario_encontrado.NombreUsuario),
        new Claim("rolUsuario", idRol.ToString()) // Agregar el id del rol como una nueva claim
    };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        AuthenticationProperties properties = new AuthenticationProperties()
                        {
                            AllowRefresh = true
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            properties);
                        usuario_encontrado.Intentos = 0;
                        _context.Update(usuario_encontrado);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        if (usuario_encontrado.Intentos <= 3)
                    {
                        try
                        {
                            // agregar un intento fallido a la tabla usuarios
                            usuario_encontrado.Intentos += 1;
                            //_context.TbUsuarios.SaveChanges(usuario_encontrado);
                            _context.Update(usuario_encontrado);
                            await _context.SaveChangesAsync();
                            ViewData["Mensaje"] = "Al tercer intento incorrecto el usuario se bloquera";
                        }
                        catch (Exception ex)
                        {
                            return BadRequest($"Error sumar intento: {ex.Message}"); // Retornar un mensaje de error
                        }
                    }
                    if (usuario_encontrado.Intentos >= 3)
                    {
                        usuario_encontrado.EstadoUsuario = 2;
                        _context.Update(usuario_encontrado);
                        await _context.SaveChangesAsync();
                        ViewData["Mensaje"] = "Contactar con el Administrador para desbloquear el usuario";
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error : {ex.Message}"); // Retornar un mensaje de error
                }
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado o bloqueado";
                return View();
            }
        }

        //public async Task<IActionResult> CerrarSeccion()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


        //    return RedirectToAction("IniciarSeccion", "Inicio");
        //    //return View();
        //}

        public async Task<IActionResult> CerrarSeccion()
        {
            if (TempData.ContainsKey("UsuarioId"))
            {
                int usuarioId = (int)TempData["UsuarioId"]; // Obtener el ID del usuario desde TempData

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                try
                {
                    // Obtener la última entrada en la tabla Bitacora para el usuario
                    TbBitacora bitacora = await _context.TbBitacoras
                        .Where(b => b.IdUsuario == usuarioId)
                        .OrderByDescending(b => b.IdBitacora)
                        .FirstOrDefaultAsync();

                    if (bitacora != null)
                    {
                        bitacora.Salida = DateTime.Now; // Actualizar la fecha de salida
                        _context.TbBitacoras.Update(bitacora);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return NotFound(); // No se encontró una entrada en la Bitacora para el usuario
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error al actualizar la bitacora: {ex.Message}"); // Retornar un mensaje de error
                }


                TempData.Remove("UsuarioId"); // Eliminar el valor de TempData después de usarlo

                return RedirectToAction("IniciarSeccion", "Inicio");
            }
            else
            {
                return BadRequest("No se pudo obtener el ID del usuario.");
            }
        }




    }

}

