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
    public class AplicacionController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/aplicacion/ingresarAplicacion")]
        public ResIngresarAplicacion ingresarAplicacion(ReqIngresarAplicacion req)
        {
            LogAplicacion logicaBackend = new LogAplicacion();
            return logicaBackend.ingresarAplicacion(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/aplicacion/obtenerAplicacionesUsuario")]
        public ResObtenerAplicaciones obtenerAplicaciones(ReqObtenerAplicacion req)
        {
            if (req == null)
            {

                return new ResObtenerAplicaciones
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };

            }
            LogAplicacion logicaBackend = new LogAplicacion();
            return logicaBackend.obtenerAplicacionesUsuario(req);
        }

    }
}