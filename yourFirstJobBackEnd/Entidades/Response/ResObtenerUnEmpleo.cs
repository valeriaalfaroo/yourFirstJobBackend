using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBackend.AccesoDatos;
using yourFirstJobBackend.Entidades.entities;

namespace yourFirstJobBackend.Entidades.Response
{
    public class ResObtenerUnEmpleo :ResBase
    {
       public Empleo empleo {  get; set; }
       
    }
}
