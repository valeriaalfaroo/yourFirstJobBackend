using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBackend.Entidades.entities
{
    public class Empleo
    {
        //Atributos propios
        public int idOfertas {  get; set; }
        public string tituloEmpleo { get; set; }
        public string descripcionEmpleo { get; set; }
        public string ubicacionEmpleo { get; set; }
        public string tipoEmpleo { get; set; }
        public string experiencia { get; set; }
        public DateTime fechaPublicacion { get; set; }

        //Relaciones

        //Empresa
        public Empresa empresa { get; set; }

        //Idiomas
        public List<Idiomas> lstIdiomas { get; set; }

        //Habilidades
        public List<Habilidades> lstHabilidades { get; set; }

        //Profesiones
        public List<Profesion> lstProfesiones { get; set; }

        //Archivos
        public List<ArchivosOferta> lstArchivos { get; set; }
    }
}
