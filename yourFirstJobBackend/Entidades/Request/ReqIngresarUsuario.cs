using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBackend.Entidades.entities;

namespace yourFirstJobBackend.Entidades.Request
{
    public class ReqIngresarUsuario
    {
        public Usuario usuario { get; set; }

        public string nombreUsuario { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public int telefono { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int idRegion { get; set; }
        public string contrasena { get; set; }


    }
}
