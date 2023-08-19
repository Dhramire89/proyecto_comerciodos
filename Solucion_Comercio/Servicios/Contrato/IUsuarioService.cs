using Solucion_Comercio.Models;

namespace Solucion_Comercio.Servicios.Contrato
{
    public interface IUsuarioService
    {
        TbUsuario GetUsuario(string correo);
        //Task<TbUsuario> SaveUsuario(TbUsuario modelo );
    }
}
