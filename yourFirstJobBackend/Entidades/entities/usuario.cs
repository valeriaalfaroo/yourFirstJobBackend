using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBackend.Entidades.entities
{
    public class Usuario
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
        public int estado { get; set; }


        //agregar listas
        //Idiomas

        public List<Idiomas> listaIdiomas { get; set; }

        public List<Habilidades> listaHabilidades { get; set; }
        public List<Estudios> listaEstudios { get; set; }


        public List<ArchivosUsuario> listaArchivosUsuarios { get; set; }

        public List<ExperienciaLaboral> listaExperienciaLaboral { get; set; }

    }
}
