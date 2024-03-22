using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using yourFirstJobBack.Entidades;
namespace API.Controllers

{
    public class EmpleoController
    {

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/empleo/obtenerTodosLosEmpleos")]
        public ResObtenerTodosLosEmpleos obtenerEmpleos()
        {
            LogOfertaEmpleo logicaBackend = new LogOfertaEmpleo();
            return logicaBackend.obtenerEmpleos(null);
        }
    }
} 