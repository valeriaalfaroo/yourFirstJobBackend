using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBackend.AccesoDatos;
using yourFirstJobBackend.Entidades.entities;
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
                    if (req.idOfertaEmpleo == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se recibio el empleo");
                    }
                    if (req.idUsuario == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se recibio el usuario");
                    }
                    if (String.IsNullOrEmpty(req.estadoAplicacion))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Estado de aplicacion faltante");
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

                    conexion.InsertarAplicacion(req.idUsuario,req.idOfertaEmpleo,req.estadoAplicacion, ref errorId, ref errorDescripcion, ref idReturn);

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
        
        public ResObtenerAplicaciones obtenerAplicacionesUsuario(ReqObtenerAplicacion req)
        {
           

            ResObtenerAplicaciones res = new ResObtenerAplicaciones();

            res.aplicaciones = new List<Aplicaciones>();    

            res.listaDeErrores = new List<string>();
            int? errorId = 0;
            string errorDescripcion = "";
            try
            {
                LinqDataContext conexion = new LinqDataContext();

                List<ObtenerAplicacionesUsuarioResult> aplicacionesDeBD = conexion.ObtenerAplicacionesUsuario(req.idUser, ref errorId, ref errorDescripcion).ToList();

                if (aplicacionesDeBD != null)
                {
                    if (errorId == 1)
                    {
                        //Error en base de datos
                        res.resultado = false;
                        res.listaDeErrores.Add(errorDescripcion);
                    }
                    else
                    {

                        foreach (ObtenerAplicacionesUsuarioResult cadaTC in aplicacionesDeBD)
                        {
                            res.aplicaciones.Add(this.crearAplicacion(cadaTC));
                        }

                        res.resultado = true;



                    }
                } else
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Aplicaciones nulas");
                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add(ex.ToString());

            }
            finally
            {
                //Bitacorear
            }

            return res;
        }


        #region
        private Aplicaciones crearAplicacion(ObtenerAplicacionesUsuarioResult aplicacionesDeBD)
        {
            Aplicaciones aplicacionARetornar = new Aplicaciones();

            aplicacionARetornar.idAplicacion = aplicacionesDeBD.idAplicacion;

            aplicacionARetornar.estadoAplicacion = aplicacionesDeBD.estadoAplicacion;

            Empleo empleoARetornar = new Empleo();

            empleoARetornar.tituloEmpleo = aplicacionesDeBD.tituloEmpleo;

            empleoARetornar.descripcionEmpleo = aplicacionesDeBD.descripcionEmpleo;

            aplicacionARetornar.empleo = empleoARetornar;

            // Set the Usuario property
           
            Usuario usuario = new Usuario();

            
            usuario.idUsuario = aplicacionesDeBD.idUsuario;


            aplicacionARetornar.usuario = usuario; ;

            return aplicacionARetornar;
        }

        #endregion

    }

}

