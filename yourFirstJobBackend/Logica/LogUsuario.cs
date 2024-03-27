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
        //ingresar un usuario
        public ResIngresarUsuario ingresarUsuario(ReqIngresarUsuario req)
        {


            ResIngresarUsuario res = new ResIngresarUsuario();

            try
            {
                res.resultado = false;
                res.listaDeErrores = new List<string>();
                if (req == null)
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Request nulo");
                }
                else
                {
                    if (req.usuario.idUsuario == null)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se encuentra el usuario");
                    }
                    if (String.IsNullOrEmpty(req.usuario.nombreUsuario))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Nombre de usuario faltante");
                    }
                    if (String.IsNullOrEmpty(req.usuario.apellidos))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso los apellidos");
                    }
                    if (String.IsNullOrEmpty(req.usuario.correo))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso el correo");
                    }
                    if (req.usuario.telefono == null)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso el numero de telefono");
                    }
                    if (req.usuario.fechaNacimiento == null)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso la fecha de nacimiento");
                    }
                    if (req.usuario.idRegion == null)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso id de region");
                    }
                    if (req.usuario.contrasena == null)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso la contrasena");
                    }

                    //puede ser null ya q no todos tienen web page
                    /* if (String.IsNullOrEmpty(req.Usuario.sitioWeb))
                     {
                         res.resultado = false;
                         res.listaDeErrores.Add("No se ingreso el sitio web");
                     }*/
                    if (req.usuario.fechaRegistro == null)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se encuentra la fecha de registro");
                    }
                }
                if (res.listaDeErrores.Any())
                {
                    res.resultado = false;
                }
                else
                {
                    //Llamar a la base de datos

                    LinqDataContext conexion = new LinqDataContext();
                    //faltan los ref en sp
                    int? idreturn = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";

                    conexion.InsertUsuario(req.usuario.nombreUsuario, req.usuario.apellidos, req.usuario.correo, req.usuario.telefono,
                        req.usuario.fechaNacimiento, req.usuario.idRegion, req.usuario.contrasena, ref errorId, ref errorDescripcion, ref idreturn);
                    if (idreturn == 0)
                    {
                        //Error en base de datos
                        res.resultado = false;
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
                res.resultado = false;
                res.listaDeErrores.Add(ex.ToString());

            }
            finally { //bitacora

                      }
            return res;

        }


        //traer un usuario
        public ResObtenerPerfilUsuario obtenerUsuario(ReqObtenerUsuario req)
        {
            ResObtenerPerfilUsuario res = new ResObtenerPerfilUsuario();
            res.listaDeErrores = new List<string>();

            try
            {
                LinqDataContext conexion = new LinqDataContext();

                int idUsuario = 3; //dato quemado

                ObtenerInformacionUsuarioResult usuarioBD = conexion.ObtenerInformacionUsuario(idUsuario).FirstOrDefault();

                if (usuarioBD != null)
                {
                    res.usuario = traerUsuario(usuarioBD);            
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

        
        private Usuario traerUsuario(ObtenerInformacionUsuarioResult usuarioBD)
        {
            Usuario usuarioRetornar = new Usuario();
            usuarioRetornar.nombreUsuario=usuarioBD.nombreUsuario;
            usuarioRetornar.apellidos = usuarioBD.apellidos;
            usuarioRetornar.correo = usuarioBD.correo;
            usuarioRetornar.telefono = usuarioBD.telefono;
            usuarioRetornar.fechaNacimiento = usuarioBD.fechaNacimiento;
            usuarioRetornar.sitioWeb = usuarioBD.sitioWeb;
            usuarioRetornar.nombreInstitucion = usuarioBD.nombreInstitucion;
            usuarioRetornar.gradoAcademico = usuarioBD.gradoAcademico;
            usuarioRetornar.fechaInicioEstudio = (DateTime)usuarioBD.fechaInicioEstudio;
            usuarioRetornar.fechaFinEstudio = (DateTime)usuarioBD.fechaFinEstudio;
            usuarioRetornar._nombreArchivo = usuarioBD.nombreArchivo;
            usuarioRetornar.archivo = usuarioBD.archivo;  //en entidad usuario estaba como byte, cambie a binary
            usuarioRetornar.tipo = usuarioBD.tipo;
            usuarioRetornar.categoria = usuarioBD.categoria;
            usuarioRetornar.descripcion = usuarioBD.descripcion;
            usuarioRetornar.idioma = usuarioBD.idioma;
            usuarioRetornar.nivel= usuarioBD.nivel;
            usuarioRetornar.puestoLaboral = usuarioBD.puestoLaboral;
            usuarioRetornar.nombreEmpresa = usuarioBD.nombreEmpresa;
            usuarioRetornar.responsabilidades = usuarioBD.  responsabilidades;
            usuarioRetornar.fechaInicioExperiencia = (DateTime)usuarioBD.fechaInicioExperiencia;
            usuarioRetornar.fechaFinExperiencia = (DateTime)usuarioBD.fechaFinExperiencia;
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


                DeleteUsuarioResult resultado = conexion.DeleteUsuario(idUsuario).SingleOrDefault();

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
            catch (Exception ex){

            }
            return res;

        }

    }


}
