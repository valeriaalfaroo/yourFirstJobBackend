using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBack.Entidades.entities
{
    public class BitacoraEmpresa
    {
        public int idBitacoraEmpresa { get; set; }
        public Empresa empresa { get; set; }
        public DateTime fechaHora { get; set; }
        public string descripcion { get; set; }
        public string estadoSesion { get; set; }
    }
}
