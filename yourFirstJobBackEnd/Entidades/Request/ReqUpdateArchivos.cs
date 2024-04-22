using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBackend.AccesoDatos;
using yourFirstJobBackend.Entidades.entities;

namespace yourFirstJobBackend.Entidades.Request
{
    public class ReqUpdateArchivos
    {
        public int idArchivosUsuarios { get; set; }

        public int idUsuario { get; set; }

        public string nombreArchivo { get; set; }

        public byte[] archivo { get; set; }

    }
}
