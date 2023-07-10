using Microsoft.AspNetCore.Mvc;
using Solucion_Comercio.Models;
using Solucion_Comercio.Recursos;
using Solucion_Comercio.Servicios.Contrato;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies; 
using Microsoft.AspNetCore.Authentication; 


namespace Solucion_Comercio.Controllers
{
    public class InicioController : Controller
    {

        private readonly IUsuarioService _usuarioServicio;

        public InicioController(IUsuarioService usuarioServicio) { 
            _usuarioServicio = usuarioServicio;
        
        }

        public IActionResult IniciarSeccion()
        {
            
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> IniciarSeccion(string correo, string clave)
        //{
        //   // TbUsuario usurio_encontrado = await _usuarioServicio.GetUsuario(correo, Utilidades.EncriptarClave(clave));
        //    TbUsuario usurio_encontrado = await _usuarioServicio.GetUsuario(correo, clave);


        //    if (usurio_encontrado == null) {
        //        ViewData["Mensaje"] = "No se encontraron conincidencias"; 
        //        return View();

        //    }
        //    List<Claim> claims = new List<Claim>() { 
        //        new Claim(ClaimTypes.Name, usurio_encontrado.NombreUsuario)
        //    };

        //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    AuthenticationProperties properties = new AuthenticationProperties() { 
        //        AllowRefresh = true,

        //    };



        [HttpPost]

        public async Task<IActionResult> IniciarSeccion(string correo, string clave)
        {
            TbUsuario usurio_encontrado = await _usuarioServicio.GetUsuario(correo, clave);

            if (usurio_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            // Obtener el id del rol del usuario
            int idRol = usurio_encontrado.RolUsuario;

            List<Claim> claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name, usurio_encontrado.NombreUsuario),
        new Claim("rolUsuario", idRol.ToString()) // Agregar el id del rol como una nueva claim
    };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            // Resto del código...
        








        await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                properties);

         
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> CerrarSeccion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSeccion", "Inicio");
            //return View();
        }

    }
}
