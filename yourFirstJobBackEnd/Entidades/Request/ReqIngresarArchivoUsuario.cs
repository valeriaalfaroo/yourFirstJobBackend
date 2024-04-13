using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBackend.Entidades.entities;

namespace yourFirstJobBackend.Entidades.Request
{
    public class ReqIngresarArchivoUsuario
    {
        //  public ArchivosUsuario archviosUsuario { get; set; }
        public int idArchivosUsuarios { get; set; }
        public string nombreArchivo { get; set; }

        public byte[] archivo { get; set; }

        public string tipo { get; set; }

        public int idUsuario { get; set; }

    }
}
