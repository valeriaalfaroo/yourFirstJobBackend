using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
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
        public DateTime fechaNacimiento { get; set; }
        public int idRegion { get; set; }
        public string contrasena { get; set; }
        public string sitioWeb { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string nombreInstitucion { get; set; }

        public string gradoAcademico { get; set; }

        public DateTime fechaInicioEstudio { get; set; }

        public DateTime fechaFinEstudio { get; set; }

        public string _nombreArchivo { get; set; }

        public Binary archivo { get; set; }

        public string tipo { get; set; }

        public string categoria { get; set; }

        public string descripcion { get; set; }

        public string idioma { get; set; }

        public string nivel { get; set; }

        public string puestoLaboral { get; set; }

        public string nombreEmpresa { get; set; }

        public string responsabilidades { get; set; }

        public DateTime fechaInicioExperiencia { get; set; }

        public DateTime fechaFinExperiencia { get; set; }


    }
}
