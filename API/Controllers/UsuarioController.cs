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

        //Insert idioma usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/insertIdiomaUsuario")]
        public ResIngresarIdiomaUsuario ingresarIdiomaUsuario(ReqIngresarIdiomaUsuario req)
        {
            if (req == null)
            {
                return new ResIngresarIdiomaUsuario
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };
            }

            LogUsuario logica = new LogUsuario();
            return logica.IngresarIdiomaUsuario(req);
        }

        //Obtener un usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/obtenerUsuario")]
        public ResObtenerPerfilUsuario obtenerUsuario(ReqObtenerUsuario req)
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

        //Delete idioma usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/eliminarIdiomaUsuario")]
        public ResEliminarIdiomaUsuario deleteIdiomaUsuario(ReqEliminarIdiomaUsuario req)
        {
            if (req == null)
            {
                return new ResEliminarIdiomaUsuario
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };
            }

            LogUsuario logica = new LogUsuario();
            return logica.eliminarIdiomaUsuario(req);
        }


        //Update usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/actualizarUsuario")]
        public ResUpdateUsuario actualizarUsuario(ReqUpdateUsuario req)
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
        public ResUpdateUsuarioIdioma actualizarIdiomaUsuario(List<ReqUpdateUsuarioIdioma> req)
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

        //Update habilidad usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/actualizarUsuarioHabilidad")]
        public ResUpdateUsuarioHabilidades actualizarHabilidad(List<ReqUpdateUsuarioHabilidades> req)
        {
            if (req == null)
            {
                return new ResUpdateUsuarioHabilidades
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };
            }

            LogUsuario logica = new LogUsuario();
            return logica.updateHabilidadesUsuario(req);
        }

        //Update estudio usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/actualizarUsuarioEstudio")]
        public ResUpdateUsuarioEstudios actualizarHabilidad(List<ReqUpdateUsuarioEstudios> req)
        {
            if (req == null)
            {
                return new ResUpdateUsuarioEstudios
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };
            }

            LogUsuario logica = new LogUsuario();
            return logica.updateEstudiosUsuario(req);
        }

        //Update experiencia usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/actualizarUsuarioExperiencia")]
        public ResUpdateUsuarioExperiencia actualizarHabilidad(List<ReqUpdateUsuarioExperiencia> req)
        {
            if (req == null)
            {
                return new ResUpdateUsuarioExperiencia
                {
                    resultado = false,
                    listaDeErrores = new List<string> { "Request nulo" }
                };
            }

            LogUsuario logica = new LogUsuario();
            return logica.updateExperienciaUsuario(req);
        }


    }
}