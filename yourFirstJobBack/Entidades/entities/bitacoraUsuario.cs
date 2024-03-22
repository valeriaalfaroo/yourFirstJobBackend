using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBack.Entidades.entities
{
    public class bitacoraUsuario
    {
        public int idBitacoraUsuario { get; set; }
        public usuario usuario { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaHora { get; set; }
        public string estadoSesion { get; set; }
    }
}
