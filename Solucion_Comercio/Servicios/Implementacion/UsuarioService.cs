using Microsoft.EntityFrameworkCore;
using Solucion_Comercio.Models;
using Solucion_Comercio.Servicios.Contrato;
using System.Security.AccessControl;

namespace Solucion_Comercio.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {

        private readonly BdcomercioContext _dbContext;



        public UsuarioService(BdcomercioContext dbContext)
        {
            _dbContext = dbContext;
        }



        //public async Task<TbUsuario> GetUsuario(string correo, string clave, int estado)
        //{
        //    TbUsuario usuario_encontrado = await _dbContext.TbUsuarios.Where(u => u.CorreoUsuario == correo && u.Password == clave)
        //        .FirstOrDefaultAsync();
        //    return usuario_encontrado;
        //}


        public async Task<TbUsuario> GetUsuario(string correo, string clave)
        {
            TbUsuario usuario_encontrado = await _dbContext.TbUsuarios.Where(u => u.CorreoUsuario == correo && u.Password == clave)
                .FirstOrDefaultAsync();
            return usuario_encontrado;
        }


        public Task<TbUsuario> GetUsuarioById(string correo, string clave)
        {
            throw new NotImplementedException();
        }
    }
}
