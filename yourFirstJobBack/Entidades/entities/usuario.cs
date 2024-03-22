using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBack.Entidades.entities
{
    public class usuario
    {
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public int telefono { get; set; }
        public DateOnly fechaNacimiento { get; set; }
        public int idRegion { get; set; }
        public string contrasena { get; set; }
        public string sitioWeb { get; set; }
        public DateTime fechaRegistro { get; set; }



    }
}
