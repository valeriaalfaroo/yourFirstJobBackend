using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
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
        
    }
} 