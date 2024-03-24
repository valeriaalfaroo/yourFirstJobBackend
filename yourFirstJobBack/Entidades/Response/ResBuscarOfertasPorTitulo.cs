using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBack.Entidades.entities;

namespace yourFirstJobBack.Entidades.Response
{
    internal class ResBuscarOfertasPorTitulo : ResBase {
        public List<Empleo> empleos { get; set; }
    }
}
