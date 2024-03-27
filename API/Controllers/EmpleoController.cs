using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using yourFirstJobBackend.Entidades.Request;
using yourFirstJobBackend.Entidades.Response;
using yourFirstJobBackend.Logica;
namespace API.Controllers

{
    public class EmpleoController : ApiController
    {

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/empleo/obtenerTodosLosEmpleos")]
        public ResObtenerTodosLosEmpleos obtenerEmpleos()
        {
            LogOfertaEmpleo logicaBackend = new LogOfertaEmpleo();
            return logicaBackend.obtenerTodosLosEmpleos(null);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/empleo/ingresarEmpleo")]
        public ResIngresarEmpleo ingresarEmpleo(ReqIngresarEmpleo req)
        {
            LogOfertaEmpleo logicaBackend = new LogOfertaEmpleo();
            return logicaBackend.ingresarEmpleo(null);
        }

    }
} 