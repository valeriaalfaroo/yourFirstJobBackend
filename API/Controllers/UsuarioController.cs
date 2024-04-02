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

        public ResIngresarUsuario insertUsuario([FromBody] Usuario usuario)
        {

            if (usuario == null)
            {
                return new ResIngresarUsuario
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };
            }

            LogUsuario logica = new LogUsuario(); 
            return logica.IngresarUsuario(usuario);
            

        }

        //  ver usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/ObtenerUsuario")]
        public ResObtenerPerfilUsuario obtenerUsuario([FromBody] ReqObtenerUsuario req)
        {
            if (req == null)
            {

                return new ResObtenerPerfilUsuario
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };

            }
            LogUsuario logicaBackend = new LogUsuario();
            return logicaBackend.obtenerUsuario(req);
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

        //actualizar usuario
        [System.Web.Http.HttpPatch] //patch permite actualizaciones parciales (no todos los datos)
        [System.Web.Http.Route("api/usuario/actualizarUsuario")]
        public ResUpdateUsuario actualizarUsuario([FromBody] Usuario usuario)
        {
            LogUsuario logicaBackend = new LogUsuario();
            return logicaBackend.actualizarUsuario(usuario);
        }

    }
}