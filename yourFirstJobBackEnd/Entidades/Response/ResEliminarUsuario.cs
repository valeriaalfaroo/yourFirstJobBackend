using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBackend.Entidades.Response
{
    public class ResEliminarUsuario :ResBase
    {
        public bool resultado { get; set; }
        public string mensaje { get; set; }
        public List<string> listaDeErrores { get; set; }

    }
}
