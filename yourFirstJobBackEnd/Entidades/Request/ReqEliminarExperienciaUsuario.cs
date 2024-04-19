using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBackend.Entidades.Request
{
    public class ReqEliminarExperienciaUsuario
    {
        public int idUsuario { get; set; }
        public int idExperiencia { get; set; }
    }
}
