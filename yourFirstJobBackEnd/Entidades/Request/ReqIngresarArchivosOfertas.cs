using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBackend.Entidades.Request
{
    public class ReqIngresarArchivosOfertas 
    {
        public int idOfertas {  get; set; }
        public string nombreArchivo { get; set; }
        public byte[] archivo { get; set; }
        public string tipo {  get; set; }
    }
}
