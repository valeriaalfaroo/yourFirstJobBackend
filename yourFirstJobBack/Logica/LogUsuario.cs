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
                    res.listaDeErrores.Add("");
                }
                else
                {
                    if (req.Usuario.idUsuario == null)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("no se encuentra el usuario");
                    }
                    if (String.IsNullOrEmpty(req.Usuario.nombreUsuario))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso el usuario");
                    }
                    if (String.IsNullOrEmpty(req.Usuario.apellidos))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso los apellidos");
                    }
                    if (String.IsNullOrEmpty(req.Usuario.correo))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso el correo");
                    }
                    if (req.Usuario.telefono == null)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso el numero de telefono");
                    }
                    if (req.Usuario.fechaNacimiento == null)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso la fecha de nacimiento");
                    }
                    if (req.Usuario.idRegion == null)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se ingreso id de region");
                    }
                    if (req.Usuario.contrasena == null)
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
                    if (req.Usuario.fechaRegistro == null)
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

                    conexion.InsertUsuario(req.Usuario.nombreUsuario, req.Usuario.apellidos, req.Usuario.correo, req.Usuario.telefono,
                        req.Usuario.fechaNacimiento, req.Usuario.idRegion, req.Usuario.contrasena);
                    if (idreturn == 0)
                    {
                        //Error en base de datos
                        //No se hizo la publicacion
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
            res.listaDeErrores = new List<string>();
            res.usuarios = new List<usuario>();

            try
            {
                LinqDataContext conexion = new LinqDataContext();

                int idUsuario = 1; //dato quemado
                List<ObtenerInformacionUsuarioResult> usuariosBD = conexion.ObtenerInformacionUsuario(idUsuario).ToList();

                foreach (ObtenerInformacionUsuarioResult allUsers in usuariosBD)
                {
                    
                    usuario usuarioObj = new usuario();
                    usuarioObj.nombreUsuario = allUsers.nombreUsuario;                 
                    res.usuarios.Add(usuarioObj);
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
       

        }


 }
