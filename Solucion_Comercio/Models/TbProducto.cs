using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solucion_Comercio.Models;

public partial class TbProducto
{
    public int IdProducto { get; set; }

    [Display(Name = "Nombre")]
    public string NombreProducto { get; set; } = null!;

    [Display(Name = "Cantidad")]
    public int CantidadProducto { get; set; }

    [Display(Name = "Precio")]
    public int PrecioProducto { get; set; }

    public virtual ICollection<TbCompra> TbCompras { get; set; } = new List<TbCompra>();

    public virtual ICollection<TbInventario> TbInventarios { get; set; } = new List<TbInventario>();
}
