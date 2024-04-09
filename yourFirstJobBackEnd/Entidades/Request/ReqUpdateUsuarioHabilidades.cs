using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBackend.Entidades.Request
{
    public class ReqUpdateUsuarioHabilidades
    {
        public int idUsuario { get; set; }
        public int idHabilidad { get; set; }
        public int idHabilidadNueva { get; set; }

    }
}
