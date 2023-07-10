using Microsoft.EntityFrameworkCore;
using Solucion_Comercio.Models; 


namespace Solucion_Comercio.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<TbUsuario> GetUsuario(string correo, string clave);

        //Task<TbUsuario> SaveUsuario(TbUsuario modelo );
    }
}
