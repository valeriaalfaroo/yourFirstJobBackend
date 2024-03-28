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
    public class UsuarioController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/ingresarUsuario")]
        public ResIngresarUsuario insertUsuario(ReqIngresarUsuario req)
        {
            LogUsuario logicaBackend = new LogUsuario();
            return logicaBackend.ingresarUsuario(req);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/usuario/ObtenerUsuario")]
        public ResObtenerPerfilUsuario obtenerUsuario()
        {
            LogUsuario logicaBackend = new LogUsuario();
            return logicaBackend.obtenerUsuario(null);
        }
    }
}