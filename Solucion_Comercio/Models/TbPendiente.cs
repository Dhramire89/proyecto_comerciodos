using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solucion_Comercio.Models;

public partial class TbPendiente
{
    public int IdPendiente { get; set; }


    public int IdCompra { get; set; }


    [Display(Name = "Compra")]
    public virtual TbCompra IdCompraNavigation { get; set; } = null!;
}
