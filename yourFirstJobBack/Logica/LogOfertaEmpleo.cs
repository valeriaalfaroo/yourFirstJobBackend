using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBack.Entidades.entities;
using yourFirstJobBack.Entidades.Request;
using yourFirstJobBack.Entidades.Response;
using yourFirstJobBack.AccesoDatos; 

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
                    if (req.empleo.empresa.idEmpresa == 0)
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
                    if (String.IsNullOrEmpty(req.empleo.experiencia))
                    {
                        res.resultado=false;
                        res.listaDeErrores.Add("Experiencia faltante");
                    }
                    if(req.empleo.profesion.idProfesion==0) {
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

                   
                    LinqDataContext conexion = new LinqDataContext();
                    //int? idReturn = 0;
                    //int? errorId = 0;
                   // string errorDescripcion = "";

                    // conexion a SP 

                    conexion.InsertarOfertaEmpleo(req.empleo.empresa.idEmpresa,req.empleo.tituloEmpleo,req.empleo.descripcionEmpleo, req.empleo.ubicacionEmpleo,req.empleo.tipoEmpleo,req.empleo.experiencia,req.empleo.fechaPublicacion/*ref idReturn, ref errorId, ref errorDescripcion*/);
                    //if (idReturn == 0)
                    //{
                    //    //Error en base de datos
                    //    //No se hizo la publicacion
                    //    res.resultado = false;
                    //    res.listaDeErrores.Add(errorDescripcion);
                    //}
                    //else
                    //{
                    //    res.resultado = true;
                    //}
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
                LinqDataContext conexion = new LinqDataContext();

               List<ObtenerTodasLasOfertasEmpleoResult> empleosDeBD = conexion.ObtenerTodasLasOfertasEmpleo().ToList();

              foreach (ObtenerTodasLasOfertasEmpleoResult cadaTC in empleosDeBD)
                   res.empleos.Add(this.crearEmpleo(cadaTC));

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
        private Empleo crearEmpleo(ObtenerTodasLasOfertasEmpleoResult empleosDeBD)
        {
            Empleo empleoRetornar= new Empleo();

            empleoRetornar.idOfertas = empleosDeBD.idOfertas;
            empleoRetornar.empresa.idEmpresa = (int)empleosDeBD.idEmpresa;
            empleoRetornar.tituloEmpleo = empleosDeBD.tituloEmpleo;
            empleoRetornar.descripcionEmpleo = empleosDeBD.descripcionEmpleo;
            empleoRetornar.ubicacionEmpleo = empleosDeBD.ubicacionEmpleo;
            empleoRetornar.tipoEmpleo = empleosDeBD.tipoEmpleo;
            empleoRetornar.experiencia = empleosDeBD.experiencia; 
            empleoRetornar.fechaPublicacion=(DateTime)empleosDeBD.fechaPublicacion;

           
            return empleoRetornar;    
        }
        #endregion

    }
}
