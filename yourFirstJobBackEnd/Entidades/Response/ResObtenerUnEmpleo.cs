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
        Empleo empleo {  get; set; }
        public ObtenerUnEmpleoResult Empleo { get; set; } //traer todos los campos necesarios y asignarlos a la respuesta

    }
}
