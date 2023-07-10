using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Solucion_Comercio.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Solucion_Comercio.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }
        //****************************************************************************************************************************************







        // este es el metodo que esta funcionando correcctamente pero sin la autorizacion de roles +++++++++++++++++++++++++++++++++++
        public IActionResult Index()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            String nombreUsuario = "";
            if (claimuser.Identity.IsAuthenticated)
            {
                nombreUsuario = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();

            }
            ViewData["nombreUsuario"] = nombreUsuario;
            return View();
        }









        public IActionResult Privacy()
        {
            return View();
        }

        public async Task< IActionResult> CerrarSecion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSeccion","Inicio");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}