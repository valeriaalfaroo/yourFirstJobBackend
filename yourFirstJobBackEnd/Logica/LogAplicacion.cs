using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBackend.AccesoDatos;
using yourFirstJobBackend.Entidades.Request;
using yourFirstJobBackend.Entidades.Response;

namespace yourFirstJobBackend.Logica
{
    public class LogAplicacion
    {
        public ResIngresarAplicacion ingresarAplicacion(ReqIngresarAplicacion req)
        {
            ResIngresarAplicacion res = new ResIngresarAplicacion();
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
                    if (req.aplicacion.empleo.idOfertas == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se recibio el empleo");
                    }
                    if (req.aplicacion.usuario.idUsuario == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se recibio el usuario");
                    }
                    if (String.IsNullOrEmpty(req.aplicacion.estadoAplicacion))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Estado de aplicacion faltante");
                    }
                    if (req.aplicacion.fechaAplicacion == DateTime.MinValue)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Fecha de publicacion faltante");
                    }
                }
                if (res.listaDeErrores.Any())
                {
                    res.resultado = false;
                }
                else
                {
                    //llamar base de datos 


                    LinqDataContext conexion = new LinqDataContext();
                    int? idReturn = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";

                    // conexion a SP 

                    conexion.InsertarAplicacion(req.aplicacion.usuario.idUsuario,req.aplicacion.empleo.idOfertas,req.aplicacion.estadoAplicacion,req.aplicacion.fechaAplicacion, ref errorId, ref errorDescripcion, ref idReturn);

                    if (idReturn == 0)
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
            finally
            {
                //Bitacora 
            }
            return res;
        }
        public 
        
    }
}

