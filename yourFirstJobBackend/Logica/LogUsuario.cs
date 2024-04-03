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

                    conexion.InsertUsuario(usuario.nombreUsuario, usuario.apellidos, usuario.correo, usuario.telefono,
                        usuario.fechaNacimiento, usuario.idRegion, Utilitarios.encriptar(usuario.contrasena), ref errorId, ref errorDescripcion, ref idReturn);

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

                Login_UserResult usuarioBD = conexion.Login_User(req.username, Utilitarios.encriptar(req.password), ref errorId, ref errorDescripcion, ref idReturn).FirstOrDefault();

                if (usuarioBD != null)
                {
                    //Errores
                    if (errorId != 0)
                    {
                        //Paso un error
                        res.listaDeErrores.Add(errorDescripcion);
                    }
                    else if (idReturn == 0)
                    {
                        //Vino vacio el ID Return
                        res.listaDeErrores.Add("Usuario no encontrado");
                    }
                    else
                    {
                        //Todo bien
                        res.idReturn = idReturn ?? default(int); ;
                    }

                }
                else
                {
                    //Null
                    res.listaDeErrores.Add("Usuario nulo");

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


        //eliminr usuario (en bd se hace update al campo estado =0 para decir q el usuario ya no existe)
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

        //actualizar un usuario

        /*  ////////////////////////FUNCIONAL
         *  /////////////////////////
           public ResUpdateUsuario actualizarUsuario(Usuario usuario)
        {
            ResUpdateUsuario res = new ResUpdateUsuario();
            res.listaDeErrores = new List<string>();
            int? errorId = 0;
            string errorDescripcion = "";

            try
            {
                LinqDataContext conexion = new LinqDataContext();

                int idUsuario = 10; //dato quemado

                // Obtener los datos actualizados del usuario
                string nombreUsuario = usuario.nombreUsuario, apellidos = usuario.apellidos, correo = usuario.correo, contrasena = usuario.contrasena , 
                    sitioWeb =usuario.sitioWeb , errorOccurred = ""; ;
                int telefono = usuario.telefono, idRegion = usuario.idRegion;
                DateTime fechaNacimiento=usuario.fechaNacimiento;
                int? camposActualizados = 0;
                int? errorMessage=0;

                UpdateUsuarioResult usuarioBD = conexion.UpdateUsuario(idUsuario, nombreUsuario,apellidos,correo,
                   telefono,fechaNacimiento, idRegion, contrasena, sitioWeb,  ref errorMessage, ref errorOccurred, ref camposActualizados).FirstOrDefault();

                if (usuarioBD != null && camposActualizados > 0)
                {
                    res.resultado = true;
                }
                else
                {
                    res.resultado = false;
                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error al actualizar el usuario: " + ex.Message);
            }
            return res;

        }
         
         ////////////////////////FUNCIONAL
         *  /////////////////////////
         



        ////////////////////////actualizar sp separados
         *  /////////////////////////
         */

        /*public ResUpdateUsuario actualizarUsuario(Usuario usuario)
        {
            ResUpdateUsuario res = new ResUpdateUsuario();
            res.listaDeErrores = new List<string>();

            try
            {
                LinqDataContext conexion = new LinqDataContext();
                int idUsuario = 1; // Dato quemado

                // Obtener los datos del usuario a actualizar
                string nombreUsuario = usuario.nombreUsuario, apellidos = usuario.apellidos, correo = usuario.correo, contrasena = usuario.contrasena,
                       sitioWeb = usuario.sitioWeb, errorOccurred = "";
                int telefono = usuario.telefono, idRegion = usuario.idRegion;
                DateTime fechaNacimiento = usuario.fechaNacimiento;

                // Actualizar los datos del usuario
                int? camposActualizados = 0;
                int? errorMessage = 0;
                int? errorOcurred = 0;
                string errorMensaje = "";
                UpdateUsuarioResult usuarioBD = conexion.UpdateUsuario(idUsuario, nombreUsuario, apellidos, correo,
                 telefono, fechaNacimiento, idRegion, contrasena, sitioWeb, ref errorMessage, ref errorOccurred, ref camposActualizados).FirstOrDefault();
               // conexion.UpdateUsuario(idUsuario, nombreUsuario, apellidos, correo, contrasena, sitioWeb, telefono, fechaNacimiento, idRegion, ref camposActualizados, ref errorMessage);

                if (usuarioBD != null && camposActualizados > 0)
                {
                    // Actualizar los idiomas del usuario
                    foreach (var idiomas in usuario.listaIdiomas)
                    {
                        string idioma = idiomas.idioma, nivel = idiomas.nivel;

                        Actualizar_Idiomas_UsuarioResult usuarioIdioma = conexion.Actualizar_Idiomas_Usuario(idUsuario,idioma,nivel, ref errorOcurred, ref errorMensaje, ref camposActualizados).FirstOrDefault(); 
                        if (errorMessage > 0)
                        {
                            res.listaDeErrores.Add($"Error al actualizar el idioma: {idiomas.idIdioma}");
                        }
                    }

                    // Actualizar las habilidades del usuario
                    foreach (var habilidad in usuario.listaHabilidades)
                    {
                        string categoria = habilidad.categoria, descripcion = habilidad.descripcion;

                        Actualizar_Habilidades_UsuarioResult usuarioHabilidad = (Actualizar_Habilidades_UsuarioResult)conexion.Actualizar_Habilidades_Usuario(idUsuario, categoria,descripcion,ref errorOcurred, ref errorMensaje, ref camposActualizados);
                        if (errorMessage > 0)
                        {
                            res.listaDeErrores.Add($"Error al actualizar la habilidad: {habilidad.idHabilidades}");
                        }
                    }

                    //actualizar estudios de usuario
                    foreach (var estudios in usuario.listaEstudios)
                    {
                        string nombreInstitucion = estudios.nombreInstitucion, gradoAcademico = estudios.gradoAcademico;
                        DateTime fechaInicio = estudios.fechaInicio, fechaFinalizacion = estudios.fechaFinalizacion;
                        //sp

                        if (errorMessage > 0)
                        {
                             res.listaDeErrores.Add($"Error al actualizar la habilidad: {estudios.idEstudios}");
                        }
                    }

                    //actualizar archivos de usuario
                    foreach (var archivosUsers in usuario.listaArchivosUsuarios)
                    {
                        string nombreArcivo= archivosUsers.nombreArchivo, tipo = archivosUsers.tipo;
                        Binary archivo = archivosUsers.archivo;
                        //sp


                        if (errorMessage > 0)
                        {
                            res.listaDeErrores.Add($"Error al actualizar la habilidad: {archivosUsers.idArchivosUsuarios}");
                        }
                    }

                    //actualizar experiencia Laboral usuario
                    foreach (var experienciaLabUser in usuario.listaExperienciaLaboral)
                    {
                        string puesto = experienciaLabUser.puesto, nombreEmpresa = experienciaLabUser.nombreEmpresa, responsabilidades= experienciaLabUser.responsabilidades;
                        DateTime fechaInicio = experienciaLabUser.fechaInicio, fechaFinalizacion = experienciaLabUser.fechaFinalizacion;
                        //sp


                        if (errorMessage > 0)
                        {
                            res.listaDeErrores.Add($"Error al actualizar la habilidad: {experienciaLabUser.idExperiencia}");
                        }
                    }

                    res.resultado = true;
                }
                else
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Error al actualizar el usuario principal.");
                }
            }
            catch (Exception ex)
            {
                res.listaDeErrores.Add("Error al actualizar: " + ex.Message);
            }

            return res;
        }*/


        //actualizar usuario
        public ResUpdateUsuario actualizarUsuarioCompleto(Usuario usuario)
        {
            ResUpdateUsuario res = new ResUpdateUsuario();
            res.listaDeErrores = new List<string>();

            try
            {
                LinqDataContext conexion = new LinqDataContext();
              //  int idUsuario = usuario.idUsuario; 
              int idUsuario = 1;
                //string archivoBase64 = Convert.ToBase64String(usuario.listaArchivosUsuarios.FirstOrDefault()?.archivo.ToArray());
                //byte[] archivo = Convert.FromBase64String(archivoBase64);
                Binary archivo = new Binary(usuario.listaArchivosUsuarios.FirstOrDefault()?.archivo.ToArray());

                int? camposActualizados = 0;
                int? errorMessage = 0;
                int? errorOcurred = 0;
                string errorMensaje = "";

                conexion.complete_Update_Usuario(
                    idUsuario,
                    usuario.nombreUsuario,
                    usuario.apellidos,
                    usuario.correo,
                    usuario.telefono,
                    usuario.fechaNacimiento,
                    usuario.idRegion,
                    usuario.contrasena,
                    usuario.sitioWeb,
                    usuario.listaIdiomas.FirstOrDefault()?.nivel ?? "", // Provide a default value if listaIdiomas is empty
                    usuario.listaIdiomas.FirstOrDefault()?.idioma ?? "",
                    usuario.listaHabilidades.FirstOrDefault()?.categoria ?? "",
                    usuario.listaHabilidades.FirstOrDefault()?.descripcion ?? "",
                    usuario.listaEstudios.FirstOrDefault()?.nombreInstitucion ?? "",
                    usuario.listaEstudios.FirstOrDefault()?.gradoAcademico ?? "",
                    usuario.listaEstudios.FirstOrDefault()?.idProfesion ?? 0,
                    usuario.listaEstudios.FirstOrDefault()?.fechaInicio ?? DateTime.MinValue,
                    usuario.listaEstudios.FirstOrDefault()?.fechaFinalizacion ?? DateTime.MinValue,
                    usuario.listaArchivosUsuarios.FirstOrDefault()?.nombreArchivo ?? "",
                   archivo,
//   usuario.listaArchivosUsuarios.FirstOrDefault()?.archivo != null ? usuario.listaArchivosUsuarios.FirstOrDefault()?.archivo.ToArray() : null,
                   usuario.listaArchivosUsuarios.FirstOrDefault()?.tipo ?? "",
                    usuario.listaExperienciaLaboral.FirstOrDefault()?.puesto ?? "",
                    usuario.listaExperienciaLaboral.FirstOrDefault()?.nombreEmpresa ?? "",
                    usuario.listaExperienciaLaboral.FirstOrDefault()?.responsabilidades ?? "",
                    usuario.listaExperienciaLaboral.FirstOrDefault()?.fechaInicio ?? DateTime.MinValue,
                    usuario.listaExperienciaLaboral.FirstOrDefault()?.fechaFinalizacion ?? DateTime.MinValue,
                    ref errorOcurred, ref errorMensaje, ref camposActualizados
                );

                res.resultado = true;
            }
            catch (Exception ex)
            {
                res.listaDeErrores.Add("Error al actualizar: " + ex.Message);
                res.resultado = false;
            }

            return res;
        }




    }

}