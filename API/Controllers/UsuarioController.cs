using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using yourFirstJobBackend.Entidades.entities;

using yourFirstJobBackend.Entidades.Request;
using yourFirstJobBackend.Entidades.Response;
using yourFirstJobBackend.Logica;
namespace API.Controllers
{
    public class UsuarioController : ApiController
    {
        //ingresar usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/ingresarUsuario")]
        public ResIngresarUsuario ingresarUsuario(ReqIngresarUsuario req)
        {
            LogUsuario logicaBackend = new LogUsuario();
            return logicaBackend.IngresarUsuario(req);
        }

        //Obtener un usuario
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/usuario/obtenerUsuario")]
        public ResObtenerPerfilUsuario obtenerUsuario()
        {
            LogUsuario logicaBackend = new LogUsuario();
            return logicaBackend.obtenerUsuario(null);
        }



        //Login
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/login")]
        public ResLogin loginUser(ReqLogin req)
        {
            if (req == null)
            {
                return new ResLogin
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };

            }

            LogUsuario logicaBackend = new LogUsuario();
            return logicaBackend.loginUser(req);

        }


        //delete usuario (se actualiza estado a 0)
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/usuario/eliminarUsuario")]
        public ResEliminarUsuario eliminarUsuario()
        {
            LogUsuario logicaBackend = new LogUsuario();
            return logicaBackend.eliminarUsuario(null);
        }



        //Update usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/actualizarUsuario")]
        public ResUpdateUsuario actualizarUsuario([FromBody] ReqUpdateUsuario req)
        {
            if (req == null)
            {
                return new ResUpdateUsuario
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };
            }

            LogUsuario logica = new LogUsuario();
            return logica.updateUsuario(req);
        }

        //Update idioma usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/actualizarUsuarioIdioma")]
        public ResUpdateUsuarioIdioma actualizarIdiomaUsuario([FromBody] List<ReqUpdateUsuarioIdioma> req)
        {
            if (req == null)
            {
                return new ResUpdateUsuarioIdioma
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };
            }

            LogUsuario logica = new LogUsuario();
            return logica.updateIdiomasUsuario(req);
        }


    }
}