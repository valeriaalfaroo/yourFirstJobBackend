using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace yourFirstJobBackend.Logica
{
    public class Utilitarios
    {
        public string encriptar(string password)
        {
            // Crear instancia de SHA256 para calcular el hash
            SHA256 sha256 = SHA256Managed.Create();

            // Crear instancia de codificación ASCII para convertir caracteres en bytes
            ASCIIEncoding encoding = new ASCIIEncoding();

            // Declarar arreglo de bytes para almacenar el hash
            byte[] stream = null;

            // Crear un StringBuilder para construir la representación hexadecimal del hash
            StringBuilder sb = new StringBuilder();

            // Calcular el hash SHA-256 de la contraseña y convertirlo a bytes
            stream = sha256.ComputeHash(encoding.GetBytes(password));

            // Recorrer cada byte del hash y construir su representación hexadecimal
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }

            return sb.ToString();
        }
    }
}
