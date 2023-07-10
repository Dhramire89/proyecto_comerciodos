using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solucion_Comercio.Models;

public partial class TbRole
{
    public int IdRol { get; set; }


   [Display(Name = "Roles")]
    public string NombreRol { get; set; } = null!;

    public virtual ICollection<TbUsuario> TbUsuarios { get; set; } = new List<TbUsuario>();
}
