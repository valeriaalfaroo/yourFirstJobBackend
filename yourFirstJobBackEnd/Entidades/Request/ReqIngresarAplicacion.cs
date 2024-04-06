using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBackend.Entidades.entities;

namespace yourFirstJobBackend.Entidades.Request
{
    public class ReqIngresarAplicacion
    {
        public int idUsuario { get; set; }
        public int idOfertaEmpleo { get; set; }
        public string estadoAplicacion { get; set; }
    }
}
