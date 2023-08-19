using Microsoft.AspNetCore.Mvc;
using Solucion_Comercio.Models;
using Solucion_Comercio.Servicios.Contrato;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace Solucion_Comercio.Controllers
{
    public class InicioController : Controller
    {

        //private readonly BdcomercioContext _context;

        private readonly IUsuarioService _usuarioServicio;

        public InicioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        public IActionResult IniciarSeccion()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> IniciarSeccion(string correo, string clave)
        {
            TbUsuario usuario_encontrado = _usuarioServicio.GetUsuario(correo);
            using BdcomercioContext _context = new BdcomercioContext();

            if (usuario_encontrado != null)
            {
                try
                {
                    if (usuario_encontrado.Password == clave && usuario_encontrado.EstadoUsuario == 1)
                    {
                        // Obtener el id del rol del usuario
                        int idRol = usuario_encontrado.RolUsuario;

                        //// Crear una nueva entrada en la tabla TbBitacota
                        //TbBitacora nuevaEntrada = new TbBitacora
                        //{
                        //    IdUsuario = usuario_encontrado.IdUsuario, // ID del usuario
                        //    Entrada = DateTime.Now // Fecha y hora actual
                        //};

                        //try
                        //{
                        //    // Agregar la nueva entrada a la tabla
                        //    _context.TbBitacoras.Add(nuevaEntrada);
                        //    _context.SaveChanges();
                        //}
                        //catch (Exception ex)
                        //{
                        //    return BadRequest($"Error al crear bitacoara: {ex.Message}"); // Retornar un mensaje de error
                        //}

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
                        return RedirectToAction("Index", "Home");
                    }
                    else

                        try
                        {
                            // Agregar la nueva entrada a la tabla
                            usuario_encontrado.Intentos += 1;
                            //_context.TbUsuarios.SaveChanges(usuario_encontrado);
                            _context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return BadRequest($"Error sumar intento: {ex.Message}"); // Retornar un mensaje de error
                        }

                    if (usuario_encontrado.Intentos == 3)
                    {
                        ViewData["Mensaje"] = "Usuario Bloqueado";
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







        //[HttpPost]
        //public async Task<IActionResult> IniciarSeccion(string correo, string clave, int estado)
        //{
        //    TbUsuario usurio_encontrado = await _usuarioServicio.GetUsuario(correo, clave);

        //    if (usurio_encontrado == null)
        //    {
        //        ViewData["Mensaje"] = "No se encontraron coincidencias";
        //        return View();
        //    }

        //    // Obtener el id del rol del usuario
        //    int idRol = usurio_encontrado.RolUsuario;

        //    List<Claim> claims = new List<Claim>(){
        //    new Claim(ClaimTypes.Name, usurio_encontrado.NombreUsuario),
        //    new Claim("rolUsuario", idRol.ToString()) // Agregar el id del rol como una nueva claim
        //};

        //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    AuthenticationProperties properties = new AuthenticationProperties()
        //    {
        //        AllowRefresh = true
        //    };

        //    await HttpContext.SignInAsync(
        //            CookieAuthenticationDefaults.AuthenticationScheme,
        //            new ClaimsPrincipal(claimsIdentity),
        //            properties);

        //    return RedirectToAction("Index", "Home");
        //}


        public async Task<IActionResult> CerrarSeccion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSeccion", "Inicio");
            //return View();
        }

    }

}

