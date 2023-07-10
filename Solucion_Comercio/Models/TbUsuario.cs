using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solucion_Comercio.Models;

public partial class TbUsuario
{
    public int IdUsuario { get; set; }

    [Display(Name = "Nombre")]
    public string NombreUsuario { get; set; } = null!;

    [Display(Name = "Apellido")]
    public string ApellidoUsuario { get; set; } = null!;

    [Display(Name = "Apellido")]
    public string ApellidoIiusuario { get; set; } = null!;

    [Display(Name = "Usuario")]
    public string UserName { get; set; } = null!;

    [Display(Name = "Contraseña")]
    public string Password { get; set; } = null!;

    [Display(Name = "Rol")]
    public int RolUsuario { get; set; }

    [Display(Name = "Estado")]
    public int EstadoUsuario { get; set; }

    [Display(Name = "Correo")]
    public string? CorreoUsuario { get; set; }

    [Display(Name = "Telefono")]
    public int? TelefonoUsuario { get; set; }

    [Display(Name = "Estado")]
    public virtual TbEstado EstadoUsuarioNavigation { get; set; } = null!;

    [Display(Name = "Rol")]
    public virtual TbRole RolUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<TbFactura> TbFacturas { get; set; } = new List<TbFactura>();

    

}
