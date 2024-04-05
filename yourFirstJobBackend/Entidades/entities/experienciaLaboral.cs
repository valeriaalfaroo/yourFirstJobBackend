using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBackend.Entidades.entities
{
    public class ExperienciaLaboral
    {
        public int idExperiencia {  get; set; } 
        public int idUsuario { get; set; }
        public Profesion profesion {  get; set; }
        public string puesto { get; set; }
        public string nombreEmpresa { get; set; }
        public string responsabilidades {  get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinalizacion { get; set; }
    }
}
