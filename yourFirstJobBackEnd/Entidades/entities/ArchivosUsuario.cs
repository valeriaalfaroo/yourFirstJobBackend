using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBackend.Entidades.entities
{
    public class ArchivosUsuario
    {
        public int idArchivosUsuarios {  get; set; }
        public int idUsuario { get; set; }
        public string nombreArchivo { get; set; }

        public byte[] archivo { get; set; }

        public string tipo { get; set; }



    }
}
