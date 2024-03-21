using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBack.Entidades.entities;
using yourFirstJobBack.Entidades.Request;
using yourFirstJobBack.Entidades.Response;

namespace yourFirstJobBack.Logica
{
    public class LogOfertaEmpleo
    {
        public ResIngresarEmpleo ingresarEmpleo(ReqIngresarEmpleo req){
            ResIngresarEmpleo res= new ResIngresarEmpleo();
            try
            {
                res.resultado = false; 
                res.listaDeErrores=new List<string>();
                if (req == null)
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Request nulo"); //Aca puede ir un enum 
                }
                else
                {
                    if (req.empleo.idEmpresa == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se recibio la empresa");
                    }
                    if (String.IsNullOrEmpty(req.empleo.tipoEmpleo))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Tipo de Empleo Faltante");
                    }
                    if (String.IsNullOrEmpty(req.empleo.descripcionEmpleo))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Descripcion de empleo faltante");
                    }
                    if (String.IsNullOrEmpty(req.empleo.fechaPublicacion))
                    {
                       res.resultado=false;
                       res.listaDeErrores.Add("Fecha de publicacion faltante");
                    }
                    if (String.IsNullOrEmpty(req.empleo.experiencia))
                    {
                        res.resultado=false;
                        res.listaDeErrores.Add("Experiencia faltante");
                    }
                    if(req.empleo.idProfesion==0) {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se recibio la profesion");
                    }
                }
                if (res.listaDeErrores.Any()) {
                    res.resultado = false;
                }
                else
                {
                    //llamar base de datos 

                    // da error porque no he conectado a linq
                   // ConexionLinqDataContext conexion = new ConexionLinqDataContext();
                    int? idReturn = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";

                    // conexion a SP 

                   // conexion.SP_INGRESAR_PUBLICACION(req.publicacion.idTema, req.publicacion.idUsuario, req.publicacion.titulo, req.publicacion.mensaje, ref idReturn, ref errorId, ref errorDescripcion);
                    if (idReturn == 0)
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

            }catch (Exception ex)
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

        public ResObtenerTodosLosEmpleos obtenerTodosLosEmpleos(ReqObtenerTodosLosEmpleos req)
        {
            ResObtenerTodosLosEmpleos res=new ResObtenerTodosLosEmpleos();
            res.listaDeErrores = new List<string>();
            res.empleos = new List<Empleo>();
            try
            {
               // ConexionLinqDataContext conexion = new ConexionLinqDataContext();

              //  List<SP_OBTENER_PUBLICACIONESResult> publicacionesDeBD = conexion.SP_OBTENER_PUBLICACIONES().ToList();

           //     foreach (SP_OBTENER_PUBLICACIONESResult cadaTC in publicacionesDeBD)
                 //   res.empleos.Add(this.crearEmpleo(cadaTC));

                res.resultado = true;

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

        //Factoria
        private Empleo crearEmpleo()
        {
           // sin terminar, se ocupa bd 
            return new Empleo();    
        }
        #endregion

    }
}
