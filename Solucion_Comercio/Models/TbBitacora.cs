using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solucion_Comercio.Models;

public partial class TbBitacora
{
    public int IdBitacora { get; set; }

    [Display(Name = "Usuario")]
    public int IdUsuario { get; set; }

    public DateTime Entrada { get; set; }

    public DateTime Salida { get; set; }
}
