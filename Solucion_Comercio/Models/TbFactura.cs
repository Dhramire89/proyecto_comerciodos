using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solucion_Comercio.Models;

public partial class TbFactura
{
    public int IdFactura { get; set; }

    [Display(Name = "Cliente")]
    public string NombreCliente { get; set; } = null!;

    [Display(Name = "Vendedor")]
    public int NombreUsuario { get; set; }

    [Display(Name = "Fecha")]
    public DateTime? FechaFactura { get; set; }

    [Display(Name = "Monto Colones")]
    public int? MontoColones { get; set; }

    [Display(Name = "Monto Dolares")]
    public int? MontoDolares { get; set; }

    [Display(Name = "Tarjeta")]
    public int? MontoTarjeta { get; set; }

    [Display(Name = "Total")]
    public int MontoTotal { get; set; }

    [Display(Name = "Usuario")]
    public virtual TbUsuario NombreUsuarioNavigation { get; set; } = null!;
}
