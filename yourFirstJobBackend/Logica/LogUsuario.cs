using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBackend.AccesoDatos;
using yourFirstJobBackend.Entidades.entities;
using yourFirstJobBackend.Entidades.Request;
using yourFirstJobBackend.Entidades.Response;

namespace yourFirstJobBackend.Logica
{
    public class LogUsuario
    {
        //Ingresar Usuario
        public ResIngresarUsuario IngresarUsuario(Usuario usuario)
        {
            ResIngresarUsuario res = new ResIngresarUsuario();

            try
            {
                res.resultado = false;
                res.listaDeErrores = new List<string>();

                // Validación de los campos del usuario
                if (usuario == null)
                {
                    res.listaDeErrores.Add("Request nulo");
                }
                else
                {
                    if (usuario.idUsuario == null)
                    {
                        res.listaDeErrores.Add("No se encuentra el usuario");
                    }
                    if (String.IsNullOrEmpty(usuario.nombreUsuario))
                    {
                        res.listaDeErrores.Add("Nombre de usuario faltante");
                    }
                    if (String.IsNullOrEmpty(usuario.apellidos))
                    {
                        res.listaDeErrores.Add("No se ingreso los apellidos");
                    }
                    if (String.IsNullOrEmpty(usuario.correo))
                    {
                        res.listaDeErrores.Add("No se ingreso el correo");
                    }
                    if (usuario.telefono == 0)
                    {
                        res.listaDeErrores.Add("No se ingreso el numero de telefono");
                    }
                    if (usuario.fechaNacimiento == DateTime.MinValue)
                    {
                        res.listaDeErrores.Add("No se ingreso la fecha de nacimiento");
                    }
                    if (usuario.idRegion == null)
                    {
                        res.listaDeErrores.Add("No se ingreso id de region");
                    }
                    if (String.IsNullOrEmpty(usuario.contrasena))
                    {
                        res.listaDeErrores.Add("No se ingreso la contrasena");
                    }
                    /*if (usuario.fechaRegistro == DateTime.MinValue)
                    {
                        res.listaDeErrores.Add("No se encuentra la fecha de registro");
                    }*/ //fecha lo ingresa la bd
                }

                // Si no hay errores de validación, intenta insertar el usuario en la base de datos
                if (!res.listaDeErrores.Any())
                {
                    LinqDataContext conexion = new LinqDataContext();
                    int? idReturn = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";

                    Utilitarios utl = new Utilitarios();

                    conexion.InsertUsuario(usuario.nombreUsuario, usuario.apellidos, usuario.correo, usuario.telefono,
                        usuario.fechaNacimiento, usuario.idRegion, utl.encriptar(usuario.contrasena), ref errorId, ref errorDescripcion, ref idReturn);

                    if (idReturn == 0)
                    {
                        // Error en la base de datos
                        res.listaDeErrores.Add(errorDescripcion);
                    }
                    else
                    {
                        res.resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                res.listaDeErrores.Add(ex.ToString());
            }
            finally
            {
                // Bitácora
            }

            return res;
        }

        //traer un usuario
        public ResObtenerPerfilUsuario obtenerUsuario(ReqObtenerUsuario req)
        {
            ResObtenerPerfilUsuario res = new ResObtenerPerfilUsuario();
            res.listaDeErrores = new List<string>();
            int? errorId = 0;
            string errorDescripcion = "";

            try
            {
                LinqDataContext conexion = new LinqDataContext();

                SP_InformacionUsuarioResult usuarioBD = conexion.SP_InformacionUsuario(req.idUser, ref errorId, ref errorDescripcion).FirstOrDefault();

                if (usuarioBD != null)
                {
                    //lista 
                    List<Idiomas> listaIdiomas = new List<Idiomas>();
                    List<Habilidades> listaHabilidad = new List<Habilidades>();
                    List<Estudios> listaEstudio = new List<Estudios>();
                    List<ArchivosUsuario> listaArchivoUsuario = new List<ArchivosUsuario>();
                    List<ExperienciaLaboral> listaExperienciaLaboral = new List<ExperienciaLaboral>();
                    res.resultado = true;
                    res.usuario = traerUsuario(usuarioBD);
                    res.usuario.listaIdiomas = listaIdiomas;
                    res.usuario.listaHabilidades = listaHabilidad;
                    res.usuario.listaEstudios = listaEstudio;
                    res.usuario.listaArchivosUsuarios = listaArchivoUsuario;
                    res.usuario.listaExperienciaLaboral = listaExperienciaLaboral;


                    int? errorIdIdiomas = 0;
                    string errorDescripcionIdiomas = "";

                    foreach (var idiomaInfo in conexion.Select_Idiomas_Usuario(usuarioBD.idUsuario, ref errorIdIdiomas, ref errorDescripcionIdiomas)) //agregar + controles de errores a sp
                    {
                        if (errorIdIdiomas == 1)
                        {
                            //Error SP Idiomas
                            res.resultado = false;
                            res.listaDeErrores.Add(errorDescripcionIdiomas);

                        }
                        else
                        {
                            Idiomas idioma = new Idiomas();

                            idioma.idIdioma = idiomaInfo.idIdioma;
                            idioma.idioma = idiomaInfo.idioma;
                            idioma.nivel = idiomaInfo.nivel;

                            listaIdiomas.Add(idioma); // Agrega el idioma a la lista

                        }

                    }

                    int? errorIdHabilidades = 0;
                    string errorDescripcionidHabilidad = "";

                    foreach (var habilidadInfo in conexion.Select_Habilidad_Usuario(usuarioBD.idUsuario, ref errorIdHabilidades, ref errorDescripcionidHabilidad))
                    {
                        if (errorIdHabilidades == 1)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorDescripcionidHabilidad);

                        }
                        else
                        {
                            Habilidades habilidades = new Habilidades();
                            habilidades.idHabilidades = habilidadInfo.idHabilidades;
                            habilidades.categoria = habilidadInfo.categoria;
                            habilidades.descripcion = habilidadInfo.descripcion;

                            listaHabilidad.Add(habilidades);

                        }

                    }


                    int? errorIdEstudios = 0;
                    string errorDescripcionidEstudios = "";
                    foreach (var EstudiosInfo in conexion.Select_Estudios_Usuario(usuarioBD.idUsuario, ref errorIdEstudios, ref errorDescripcionidEstudios))
                    {
                        if (errorIdEstudios == 1)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorDescripcionidEstudios);

                        }
                        else
                        {
                            Estudios estudios = new Estudios();
                            estudios.idEstudios = EstudiosInfo.idEstudios;
                            estudios.nombreInstitucion = EstudiosInfo.nombreInstitucion;
                            estudios.gradoAcademico = EstudiosInfo.gradoAcademico;
                            estudios.fechaInicio = EstudiosInfo.fechaInicio;
                            estudios.fechaFinalizacion = EstudiosInfo.fechaFinalizacion;


                            listaEstudio.Add(estudios);
                        }

                    }


                    int? errorIdArchivosU = 0;
                    string errorDescripcionidArchivosU = "";
                    foreach (var ArchivosInfo in conexion.Select_Archivos_Usuario(usuarioBD.idUsuario, ref errorIdArchivosU, ref errorDescripcionidArchivosU))
                    {
                        if (errorIdEstudios == 1)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorDescripcionidArchivosU);

                        }
                        else
                        {
                            ArchivosUsuario archivUsuario = new ArchivosUsuario();
                            archivUsuario.idArchivosUsuarios = ArchivosInfo.idArchivosUsuarios;
                            archivUsuario.idUsuario = ArchivosInfo.idUsuario;
                            archivUsuario.nombreArchivo = ArchivosInfo.nombreArchivo;
                            archivUsuario.archivo = ArchivosInfo.archivo;
                            archivUsuario.tipo = ArchivosInfo.tipo;

                            listaArchivoUsuario.Add(archivUsuario);
                        }

                    }


                    int? errorIdExLaboral = 0;
                    string errorDescripcionidExLaboral = "";
                    foreach (var ExperienciaInfo in conexion.Select_Experiencia_Laboral(usuarioBD.idUsuario, ref errorIdExLaboral, ref errorDescripcionidExLaboral))
                    {
                        if (errorIdExLaboral == 1)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorDescripcionidExLaboral);

                        }
                        else
                        {
                            ExperienciaLaboral exLaboral = new ExperienciaLaboral();
                            exLaboral.idExperiencia = ExperienciaInfo.idExperiencia;
                            exLaboral.idUsuario = ExperienciaInfo.idUsuario;
                            exLaboral.idProfesion = ExperienciaInfo.idProfesion;
                            exLaboral.puesto = ExperienciaInfo.puesto;
                            exLaboral.nombreEmpresa = ExperienciaInfo.nombreEmpresa;
                            exLaboral.responsabilidades = ExperienciaInfo.responsabilidades;
                            exLaboral.fechaInicio = ExperienciaInfo.fechaInicio;
                            exLaboral.fechaFinalizacion = ExperienciaInfo.fechaFinalizacion;

                            listaExperienciaLaboral.Add(exLaboral);
                        }

                    }



                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add(ex.ToString());

            }
            finally
            {
            }

            return res;

        }

        #region


        private Usuario traerUsuario(SP_InformacionUsuarioResult usuarioBD)
        {
            Usuario usuarioRetornar = new Usuario();
            usuarioRetornar.nombreUsuario = usuarioBD.nombreUsuario;
            usuarioRetornar.apellidos = usuarioBD.apellidos;
            usuarioRetornar.correo = usuarioBD.correo;
            usuarioRetornar.telefono = usuarioBD.telefono;
            usuarioRetornar.fechaNacimiento = usuarioBD.fechaNacimiento;
            usuarioRetornar.sitioWeb = usuarioBD.sitioWeb;
            usuarioRetornar.idRegion = usuarioBD.idRegion;
            usuarioRetornar.idUsuario = usuarioBD.idUsuario;
            usuarioRetornar.contrasena = usuarioBD.contrasena;


            return usuarioRetornar;


        }



        #endregion

        //Login
        public ResLogin loginUser(ReqLogin req)
        {
            ResLogin res = new ResLogin();

            res.listaDeErrores = new List<string>();

            int? errorId = 0;
            int? idReturn = 0;
            string errorDescripcion = "";


            try
            {
                LinqDataContext conexion = new LinqDataContext();

                Utilitarios utl = new Utilitarios();

                Login_UserResult usuarioBD = conexion.Login_User(req.username, utl.encriptar(req.password), ref errorId, ref errorDescripcion, ref idReturn).FirstOrDefault();

                if (usuarioBD != null)
                {
                    //Errores
                    if (errorId != 0)
                    {
                        //Paso un error
                        res.listaDeErrores.Add(errorDescripcion);
                        res.resultado = false;
                    }
                    else if (idReturn == 0)
                    {
                        //Vino vacio el ID Return
                        res.listaDeErrores.Add("Usuario no encontrado");
                        res.resultado = false;
                    }
                    else
                    {
                        //Todo bien
                        Usuario usuario = new Usuario();

                        usuario.idUsuario = usuarioBD.idUsuario;
                        usuario.nombreUsuario = usuarioBD.nombreUsuario;
                        usuario.apellidos = usuarioBD.apellidos;
                        usuario.correo = usuarioBD.correo;

                        res.usuario = usuario;

                        res.resultado = true;
                    }

                }
                else
                {
                    //Null
                    res.listaDeErrores.Add("Usuario nulo");
                    res.resultado = false;

                }

            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add(ex.ToString());

            }
            finally
            {

                //Bitacora

            }
            return res;
        }


        //Eliminr usuario (en bd se hace update al campo estado =0 para decir q el usuario ya no existe)
        public ResEliminarUsuario eliminarUsuario(ReqEliminarUsuario req)
        {
            ResEliminarUsuario res = new ResEliminarUsuario();
            res.listaDeErrores = new List<string>();
            try
            {
                LinqDataContext conexion = new LinqDataContext();
                //  int idUsuario = req.idUsuario;  Obtener el idUsuario desde el objeto req
                int idUsuario = 10; //dato quemado
                int? errorOccured = 0;
                string errorMessage = "";
                int? lineasActualizadas = 0;


                DesactivarUsuarioResult resultado = conexion.DesactivarUsuario(idUsuario, ref errorOccured, ref errorMessage, ref lineasActualizadas).SingleOrDefault();

                if (resultado != null)
                {
                    res.resultado = true;
                    // res.mensaje = resultado.ErrorMessage; 
                }
                else
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("No se encontró el usuario.");
                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add(ex.ToString());
            }
            return res;


        }

        //Update usuario
        public ResUpdateUsuario actualizarUsuarioCompleto(ReqUpdateUsuario req)
        {
            ResUpdateUsuario res = new ResUpdateUsuario();
            res.listaDeErrores = new List<string>();
            res.resultado=false;

            try
            {
                if (req != null && req.usuario != null)
                {
                    res.usuario = req.usuario;
                    LinqDataContext conexion = new LinqDataContext();
              //  int idUsuario = usuario.idUsuario; 
              int idUsuario = 1;
                //string archivoBase64 = Convert.ToBase64String(usuario.listaArchivosUsuarios.FirstOrDefault()?.archivo.ToArray());
                //byte[] archivo = Convert.FromBase64String(archivoBase64);
              //  Binary archivo = new Binary(usuario.listaArchivosUsuarios.FirstOrDefault()?.archivo.ToArray());

                int? camposActualizados = 0;
                int? errorMessage = 0;
                int? errorOcurred = 0;
                string errorMensaje = "";

                conexion.complete_Update_Usuario(
                    idUsuario,
                    res.usuario.nombreUsuario,
                    res.usuario.apellidos,
                    res.usuario.correo,
                    res.usuario.telefono,
                    res.usuario.fechaNacimiento,
                    res.usuario.idRegion,
                    res.usuario.contrasena,
                    res.usuario.sitioWeb,
                    res.usuario.listaIdiomas.FirstOrDefault()?.nivel ?? "", // Provide a default value if listaIdiomas is empty
                    res.usuario.listaIdiomas.FirstOrDefault()?.idioma ?? "",
                    res.usuario.listaHabilidades.FirstOrDefault()?.categoria ?? "",
                    res.usuario.listaHabilidades.FirstOrDefault()?.descripcion ?? "",
                    res.usuario.listaEstudios.FirstOrDefault()?.nombreInstitucion ?? "",
                    res.usuario.listaEstudios.FirstOrDefault()?.gradoAcademico ?? "",
                    res.usuario.listaEstudios.FirstOrDefault()?.idProfesion ?? 0,
                    res.usuario.listaEstudios.FirstOrDefault()?.fechaInicio ?? DateTime.MinValue,
                    res.usuario.listaEstudios.FirstOrDefault()?.fechaFinalizacion ?? DateTime.MinValue,
                    res.usuario.listaArchivosUsuarios.FirstOrDefault()?.nombreArchivo ?? "",
                    res.usuario.listaArchivosUsuarios.FirstOrDefault()?.archivo != null ? res.usuario.listaArchivosUsuarios.FirstOrDefault()?.archivo.ToArray() : null,
                    res.usuario.listaArchivosUsuarios.FirstOrDefault()?.tipo ?? "",
                    res.usuario.listaExperienciaLaboral.FirstOrDefault()?.puesto ?? "",
                    res.usuario.listaExperienciaLaboral.FirstOrDefault()?.nombreEmpresa ?? "",
                    res.usuario.listaExperienciaLaboral.FirstOrDefault()?.responsabilidades ?? "",
                    res.usuario.listaExperienciaLaboral.FirstOrDefault()?.fechaInicio ?? DateTime.MinValue,
                    res.usuario.listaExperienciaLaboral.FirstOrDefault()?.fechaFinalizacion ?? DateTime.MinValue,
                    ref errorOcurred, ref errorMensaje, ref camposActualizados
                );
                    if (errorOcurred == 0)
                    {
                        res.usuario = req.usuario;
                        res.resultado = true;
                    }
                    else
                    {
                        res.listaDeErrores.Add(errorMensaje);
                    }
                }
                else
                {
                    res.listaDeErrores.Add("Error al actualizar: El objeto usuario es nulo.");
                }

            }
            catch (Exception ex)
            {
                res.listaDeErrores.Add("Error al actualizar: " + ex.Message);
            }

            return res;
        }




    }

}