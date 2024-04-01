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
        //Obtener Todos los empleos
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/empleo/obtenerTodosLosEmpleos")]
        public ResObtenerTodosLosEmpleos obtenerEmpleos()
        {
            LogOfertaEmpleo logicaBackend = new LogOfertaEmpleo();
            return logicaBackend.obtenerTodosLosEmpleos(null);
        }

        //Ingresar empleo
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/empleo/ingresarEmpleo")]
        public ResIngresarEmpleo ingresarEmpleo(ReqIngresarEmpleo req)
        {
            LogOfertaEmpleo logicaBackend = new LogOfertaEmpleo();
            return logicaBackend.ingresarEmpleo(req);
        }

        //Buscar empleo por titulo
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/empleo/obtenerEmpleosTitulo")]
        public ResBuscarOfertasPorTitulo buscarOfertasEmpleoPorTitulo([FromBody] ReqBuscarOfertasPorTitulo req)
        {

            if (req == null)
            {

                return new ResBuscarOfertasPorTitulo
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };

            }


            LogOfertaEmpleo logicaBackend = new LogOfertaEmpleo();
            return logicaBackend.buscarOfertasEmpleoPorTitulo(req);
        }

    }
} 