using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Solucion_Comercio.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Data;
using System.Data.SqlClient;

namespace Solucion_Comercio.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly string cadenaSql;


        public HomeController(IConfiguration confg)
        {
            cadenaSql = confg.GetConnectionString("Conexion");

        }


        //****************************************************************************************************************************************

        //private readonly ILogger<HomeController> _logger;


        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;

        //}


        // este es el metodo que esta funcionando correcctamente +++++++++++++++++++++++++++++++++++
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




        [HttpGet]
        public JsonResult BusquedaControl(string busqueda)
        {
            List<Busqueda> lista = new List<Busqueda>();


            using (var conexion = new SqlConnection(cadenaSql)) { 
                conexion.Open();    
                SqlCommand cmd = new SqlCommand("sp_busqueda", conexion);
                cmd.Parameters.AddWithValue("busqueda", busqueda);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader()){

                    while (dr.Read()){
                        lista.Add(new Busqueda(){
                            value = Convert.ToInt32(dr["idProducto"]),
                            nombreProducto = dr["nombreProducto"].ToString(),
                            label = dr["Texto"].ToString(),
                            precioProducto = Convert.ToDecimal(dr["precioProducto"])
                            
                        }
                        );
                    }

                }
            }

                return Json(lista);
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