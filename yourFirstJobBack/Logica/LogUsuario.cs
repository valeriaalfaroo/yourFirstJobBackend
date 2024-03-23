using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBack.AccesoDatos;
using yourFirstJobBack.Entidades.entities;
using yourFirstJobBack.Entidades.Request;
using yourFirstJobBack.Entidades.Response;

namespace yourFirstJobBack.Logica
{
    public class LogUsuario
    {
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
                        req.usuario.fechaNacimiento, req.usuario.idRegion, req.usuario.contrasena);
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
        
        public ResObtenerPerfilUsuario obtenerUsuario(ReqObtenerUsuario req)
        {
            ResObtenerPerfilUsuario res = new ResObtenerPerfilUsuario();
            Usuario usuario = new Usuario();
            res.listaDeErrores = new List<string>();
            res.usuario = usuario;


            try
            {
                LinqDataContext conexion = new LinqDataContext();

                int idUsuario = 1; //dato quemado
                ObtenerInformacionUsuarioResult usuarioBD = conexion.ObtenerInformacionUsuario(idUsuario).SingleOrDefault();

                if (usuarioBD != null)
                {

                    
                    Usuario usuarioObj = new Usuario();

                    usuarioObj.nombreUsuario = allUsers.nombreUsuario;  
                    
                    res.usuario = usuarioObj;
                    //faltan los demas atributos?? 

               
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

        
        private usuario traerUsuario(ObtenerInformacionUsuarioResult usuarioBD)
        {
            usuario usuarioRetornar = new usuario();
            usuarioRetornar.nombreUsuario=usuarioRetornar.nombreUsuario;
            usuarioRetornar.apellidos = usuarioRetornar.apellidos;
            usuarioRetornar.correo=usuarioRetornar.correo;
            usuarioRetornar.telefono = usuarioRetornar.telefono;
            usuarioRetornar.fechaNacimiento = usuarioRetornar.fechaNacimiento;
            usuarioRetornar.sitioWeb = usuarioRetornar.sitioWeb;
            usuarioRetornar.nombreInstitucion = usuarioRetornar.nombreInstitucion;
            usuarioRetornar.gradoAcademico = usuarioRetornar.gradoAcademico;
            usuarioRetornar.fechaInicioEstudio = usuarioRetornar.fechaInicioEstudio;
            usuarioRetornar.fechaFinEstudio = usuarioRetornar.fechaFinEstudio;
            usuarioRetornar._nombreArchivo = usuarioRetornar._nombreArchivo;
            usuarioRetornar.archivo = usuarioRetornar.archivo;
            usuarioRetornar.tipo = usuarioRetornar.tipo;
            usuarioRetornar.categoria = usuarioRetornar.categoria;
            usuarioRetornar.descripcion = usuarioRetornar.descripcion;
            usuarioRetornar.idioma = usuarioRetornar.idioma;
            usuarioRetornar.descripcion = usuarioRetornar.descripcion;
            usuarioRetornar.puestoLaboral = usuarioRetornar.puestoLaboral;
            usuarioRetornar.nombreEmpresa = usuarioRetornar.nombreEmpresa;
            usuarioRetornar.fechaInicioExperiencia= usuarioRetornar.fechaInicioExperiencia;
            usuarioRetornar.fechaFinExperiencia = usuarioRetornar.fechaFinExperiencia;

            return usuarioRetornar;


        }
        #endregion


    }


}
