using Microsoft.EntityFrameworkCore;
using Solucion_Comercio.Models;
using Solucion_Comercio.Servicios.Contrato;

namespace Solucion_Comercio.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly BdcomercioContext _dbContext;
        public UsuarioService(BdcomercioContext dbContext)
        {
            _dbContext = dbContext;
        }                  
        public  TbUsuario GetUsuario(string correo)
        {
            TbUsuario usuario_encontrado = new TbUsuario(); 
             usuario_encontrado =  _dbContext.TbUsuarios.FirstOrDefault(u => u.CorreoUsuario == correo);
            return usuario_encontrado;
        }

        

        public Task<TbUsuario> GetUsuarioById(string correo, string clave)
        {
            throw new NotImplementedException();
        }
    }
}
