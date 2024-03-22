using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBack.Entidades.entities
{
    public class Aplicaciones
    {
        public int idAplicacion {  get; set; }
        public usuario usuario { get; set; }    
        public Empleo empleo { get; set; }
        public string estadoAplicacion { get; set; }
        public DateTime fechaAplicacion { get; set; }
    }
}
