using System;
using System.Collections.Generic;
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
                        usuario.fechaNacimiento, usuario.idRegion, usuario.contrasena, ref errorId, ref errorDescripcion, ref idReturn);

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

                int idUsuario = 3; //dato quemado

                SP_InformacionUsuarioResult usuarioBD = conexion.SP_InformacionUsuario(idUsuario, ref errorId, ref errorDescripcion).FirstOrDefault();

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

                    foreach (var idiomaInfo in conexion.Select_Idiomas_Oferta(usuarioBD.idUsuario, ref errorIdIdiomas, ref errorDescripcionIdiomas))
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
                    foreach (var ArchivosInfo in conexion.Select_Archivos_Usuario(usuarioBD.idUsuario, ref errorIdEstudios, ref errorDescripcionidEstudios))
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




        //eliminr usuario
        public ResEliminarUsuario eliminarUsuario(ReqEliminarUsuario req)
        {
            ResEliminarUsuario res = new ResEliminarUsuario();
            res.listaDeErrores = new List<string>();
            try
            {
                LinqDataContext conexion = new LinqDataContext();
                //  int idUsuario = req.idUsuario;  Obtener el idUsuario desde el objeto req
                int idUsuario = 1; //dato quemado
                int? errorOccured =0;
                string errorMessage ="";
                int? lineasBorradas=0;


                DeleteUsuarioResult resultado = conexion.DeleteUsuario(idUsuario, ref errorOccured, ref errorMessage, ref lineasBorradas).SingleOrDefault();

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



            //actualizar un usuario
        }
        public ResUpdateUsuario actualizarUsuario(ReqUpdateUsuario req)
        {
            ResUpdateUsuario res = new ResUpdateUsuario();
            res.listaDeErrores = new List<string>();

            try
            {

            }
            catch (Exception ex)
            {

            }
            return res;

        }

    }


}
