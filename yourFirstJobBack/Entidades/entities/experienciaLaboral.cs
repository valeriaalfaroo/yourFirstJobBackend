using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBack.Entidades.entities
{
    public class ExperienciaLaboral
    {
        public int idExperiencia {  get; set; }
        public Usuario usuario { get; set; }

        public Profesion profesion {  get; set; }
        public string puesto { get; set; }
        public string nombreEmpresa { get; set; }
        public string responsabilidades {  get; set; }

        public DateOnly fechaInicio { get; set; }
        public DateOnly fechaFinalizacion { get; set; }
    }
}
