using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using yourFirstJobBackend.Entidades.Response;
using yourFirstJobBackend.Logica;
namespace API.Controllers

{
    public class EmpleoController
    {

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/empleo/obtenerTodosLosEmpleos")]
        public ResObtenerTodosLosEmpleos obtenerEmpleos()
        {
            LogOfertaEmpleo logicaBackend = new LogOfertaEmpleo();
            return logicaBackend.obtenerTodosLosEmpleos(null);
        }
        
    }
} 