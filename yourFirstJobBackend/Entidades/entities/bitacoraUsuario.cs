using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBackend.Entidades.entities
{
    public class BitacoraUsuario
    {
        public int idBitacoraUsuario { get; set; }
        public Usuario usuario { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaHora { get; set; }
        public string estadoSesion { get; set; }
    }
}
