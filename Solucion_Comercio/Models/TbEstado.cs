using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solucion_Comercio.Models;

public partial class TbEstado
{
    public int IdEstado { get; set; }

    [Display(Name = "Estado")]
    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<TbUsuario> TbUsuarios { get; set; } = new List<TbUsuario>();
}
