
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Cryptography;
using System.Text; 

namespace Solucion_Comercio.Recursos
{
    public class Utilidades
    {

        public static string EncriptarClave(string clave)
        {
            StringBuilder sb = new StringBuilder();

            using (MD5 md5 = MD5.Create()) {
                Encoding enc = Encoding.UTF8; 


                byte[] result = md5.ComputeHash(enc.GetBytes(clave));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }

        }


    }
}
