using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solucion_Comercio.Models;

public partial class TbInventario
{
    public int IdInventario { get; set; }

    [Display(Name = "Producto")]
    public int IdProducto { get; set; }

    public int Existencia { get; set; }

    [Display(Name = "Producto")]
    public virtual TbProducto IdProductoNavigation { get; set; } = null!;
}
