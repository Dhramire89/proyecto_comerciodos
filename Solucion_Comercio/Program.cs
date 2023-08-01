using Microsoft.EntityFrameworkCore;
using Solucion_Comercio.Models;

using Solucion_Comercio.Servicios.Contrato;
using Solucion_Comercio.Servicios.Implementacion;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Optivem.Framework.Core.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<BdcomercioContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
}
);


builder.Services.AddScoped<IUsuarioService, UsuarioService>(); // permite el uso del servicio dentro de cualquier controlador 




// ... Configuracion de las politicas de acceso a las pantallas   ...

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PoliticaSalonero", policy =>
        policy.RequireClaim("rolUsuario", "1", "3"));

    options.AddPolicy("PoliticaCajero", policy =>
        policy.RequireClaim("rolUsuario", "2", "3"));

    options.AddPolicy("PoliticaAdiministrador", policy =>
        policy.RequireClaim("rolUsuario", "3")); 

    options.AddPolicy("PoliticaCocinero", policy =>
        policy.RequireClaim("rolUsuario", "1002", "3"));


});














builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/IniciarSeccion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.Cookie.Name = "MyCoookie";
        options.AccessDeniedPath = "/Home/Privacy";
    });

//metodo para borrar cache y que no permita volver cuando se cerro secion 
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(
        new ResponseCacheAttribute { 
            NoStore = true,
            Location= ResponseCacheLocation.None,
        }
        );

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Privacy");
}
app.UseStaticFiles();
app.UseRouting();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSeccion}/{id?}");

app.Run();