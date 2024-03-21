using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBack.Entidades.entities
{
    internal class Empleo
    {
        public int idOfertas {  get; set; }
        public int idEmpresa { get; set; }
        public string tituloEmpleo { get; set; }
        public string descripcionEmpleo { get; set; }
        public string ubicacionEmpleo { get; set; }
        public int idProfesion {  get; set; }
        public string tipoEmpleo { get; set; }
        public string experiencia { get; set; }
        public string fechaPublicacion { get; set; }


    }
}
