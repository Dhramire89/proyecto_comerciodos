using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solucion_Comercio.Models;

public partial class TbCompra
{
    public int IdCompra { get; set; }

    [Display(Name = "Usuario")]
    public string NombreUsuario { get; set; } = null!;

    [Display(Name = "Fecha")]
    public DateTime FechaCompra { get; set; }

    [Display(Name = "Producto")]
    public int IdProducto { get; set; }

    [Display(Name = "Cantidad")]
    public int CantidadCompra { get; set; }

[Display(Name = "Producto")]
    public virtual TbProducto IdProductoNavigation { get; set; } = null!;

    public virtual ICollection<TbPendiente> TbPendientes { get; set; } = new List<TbPendiente>();
}
