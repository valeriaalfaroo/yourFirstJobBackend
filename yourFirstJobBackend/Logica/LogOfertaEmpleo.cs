using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBackend.Entidades.entities;
using yourFirstJobBackend.Entidades.Request;
using yourFirstJobBackend.Entidades.Response;
using yourFirstJobBackend.AccesoDatos; 

namespace yourFirstJobBackend.Logica
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

                    conexion.InsertarOfertaEmpleo(req.empleo.empresa.idEmpresa, req.empleo.tituloEmpleo, req.empleo.descripcionEmpleo, req.empleo.ubicacionEmpleo, req.empleo.tipoEmpleo, req.empleo.experiencia, req.empleo.fechaPublicacion/*ref idReturn, ref errorId, ref errorDescripcion*/);
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
            res.empleos = new List<Empleo>();

            res.listaDeErrores = new List<string>();
            int? errorId = 0;
            string errorDescripcion = "";

            try
            {
                LinqDataContext conexion = new LinqDataContext();

                List<ObtenerTodasLasOfertasEmpleoResult> empleosDeBD = conexion.ObtenerTodasLasOfertasEmpleo(ref errorId, ref errorDescripcion).ToList();

                if (errorId == 1) {
                    //Error en base de datos
                    res.resultado = false;
                    res.listaDeErrores.Add(errorDescripcion);
                }
                else {

                    //Todo funciona
                    res.resultado = true;

                    foreach (ObtenerTodasLasOfertasEmpleoResult cadaTC in empleosDeBD) {
                        //Cada oferta

                        //Saco Idiomas
                        int? errorIdIdiomas = 0;
                        string errorDescripcionIdiomas = "";

                        List<Idiomas> lstIdiomasDeCada = new List<Idiomas>();

                        foreach (var idiomaInfo in conexion.Select_Idiomas_Oferta(cadaTC.idOfertas, ref errorIdIdiomas, ref errorDescripcionIdiomas))
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

                                lstIdiomasDeCada.Add(idioma); // Agrega el idioma a la lista

                            }
                            
                        }

                        //Saco Profesion
                        int? errorIdProfesion = 0;
                        string errorDescripcionProfesion = "";

                        List<Profesion> lstProfesionDeCada = new List<Profesion>();

                        foreach (var profesionInfo in conexion.Select_Profeciones_Oferta(cadaTC.idOfertas, ref errorIdProfesion, ref errorDescripcionProfesion))
                        {
                            if (errorIdProfesion == 1)
                            {
                                //Error SP Idiomas
                                res.resultado = false;
                                res.listaDeErrores.Add(errorDescripcionProfesion);

                            }
                            else
                            {
                                Profesion profesion = new Profesion();

                                profesion.idProfesion = profesionInfo.idProfesion;
                                profesion.nombreProfesion = profesionInfo.nombreProfesion;
                                profesion.descripcion = profesionInfo.descripcion;

                                lstProfesionDeCada.Add(profesion); // Agrega la profesion a la lista

                            }

                        }

                        //Saco Habilidades
                        int? errorIdHabilidades = 0;
                        string errorDescripcionHabilidades = "";

                        List<Habilidades> lstHabilidadesDeCada = new List<Habilidades>();

                        foreach (var habilidadesInfo in conexion.Select_Habilidades_Oferta(cadaTC.idOfertas, ref errorIdHabilidades, ref errorDescripcionHabilidades))
                        {
                            if (errorIdHabilidades == 1)
                            {
                                //Error SP Idiomas
                                res.resultado = false;
                                res.listaDeErrores.Add(errorDescripcionHabilidades);

                            }
                            else
                            {
                                Habilidades habilidades = new Habilidades();

                                habilidades.idHabilidades = habilidadesInfo.idHabilidades;
                                habilidades.descripcion = habilidadesInfo.descripcion;
                                habilidades.categoria = habilidadesInfo.categoria;

                                lstHabilidadesDeCada.Add(habilidades); // Agrega la habilidad a la lista

                            }

                        }


                        //Meto lista
                        res.empleos.Add(this.crearEmpleo(cadaTC, lstIdiomasDeCada, lstProfesionDeCada, lstHabilidadesDeCada));


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
                //Bitacorear
            }
            return res; 
        }



        public ResBuscarOfertasPorTitulo buscarOfertasEmpleoPorTitulo(ReqBuscarOfertasPorTitulo req)
        {

            ResBuscarOfertasPorTitulo res = new ResBuscarOfertasPorTitulo();

            res.listaDeErrores = new List<string>();

            res.empleos = new List<Empleo>();

            try
            {
                LinqDataContext conexion = new LinqDataContext();

                string tituloEmpleo = "Electrisista"; //Titulo quemado temporalmente

                List<BuscarOfertasEmpleoPorTituloResult> empleosDeBD = conexion.BuscarOfertasEmpleoPorTitulo(tituloEmpleo).ToList();

                foreach (BuscarOfertasEmpleoPorTituloResult cadaTC in empleosDeBD)
                    res.empleos.Add(this.crearEmpleoT(cadaTC));

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

        //Factoria Todos los Empleos
        private Empleo crearEmpleo(ObtenerTodasLasOfertasEmpleoResult empleosDeBD, List<Idiomas>lstIdiomas, List<Profesion> lstProfesiones, List<Habilidades> lstHabilidades)
        {
            Empleo empleoRetornar= new Empleo();

            empleoRetornar.idOfertas = empleosDeBD.idOfertas;
            empleoRetornar.tituloEmpleo = empleosDeBD.tituloEmpleo;
            empleoRetornar.descripcionEmpleo = empleosDeBD.descripcionEmpleo;
            empleoRetornar.ubicacionEmpleo = empleosDeBD.ubicacionEmpleo;
            empleoRetornar.tipoEmpleo = empleosDeBD.tipoEmpleo;
            empleoRetornar.experiencia = empleosDeBD.experiencia; 
            empleoRetornar.fechaPublicacion=(DateTime)empleosDeBD.fechaPublicacion;

            //Empresa
            Empresa empresaRetornar = new Empresa();

            
            empresaRetornar.idEmpresa = empleosDeBD.idEmpresa;
            empresaRetornar.nombreEmpresa = empleosDeBD.nombreEmpresa;
            empresaRetornar.telefonoEmpresa = empleosDeBD.telefonoEmpresa;
            empresaRetornar.cedulaJuridica = empleosDeBD.cedulaJuridica;
            empresaRetornar.descripcion = empleosDeBD.descripcion;
            empresaRetornar.fechaRegistro = empleosDeBD.fechaRegitro;
            
            //Region
            Region regionRetornar = new Region();
            regionRetornar.idRegion = empleosDeBD.idRegion;
            regionRetornar.nombreRegion = empleosDeBD.nombreRegion;

            //Meto region a empresa
            empresaRetornar.region = regionRetornar;

            //Meto empresa a oferta 
            empleoRetornar.empresa = empresaRetornar;

            //Idiomas
            empleoRetornar.lstIdiomas = lstIdiomas;

            //Profesiones
            empleoRetornar.lstProfesiones = lstProfesiones;

            //Habilidades
            empleoRetornar.lstHabilidades = lstHabilidades;


            return empleoRetornar;    
        }

        //Factoria Todos los Empleos
        private Empleo crearEmpleoT(BuscarOfertasEmpleoPorTituloResult empleosDeBD)
        {
            Empleo empleoRetornar = new Empleo();

            empleoRetornar.idOfertas = empleosDeBD.idOfertas;
            empleoRetornar.empresa.idEmpresa = (int)empleosDeBD.idEmpresa;
            empleoRetornar.tituloEmpleo = empleosDeBD.tituloEmpleo;
            empleoRetornar.descripcionEmpleo = empleosDeBD.descripcionEmpleo;
            empleoRetornar.ubicacionEmpleo = empleosDeBD.ubicacionEmpleo;
            empleoRetornar.tipoEmpleo = empleosDeBD.tipoEmpleo;
            empleoRetornar.experiencia = empleosDeBD.experiencia;
            empleoRetornar.fechaPublicacion = (DateTime)empleosDeBD.fechaPublicacion;


            return empleoRetornar;
        }

        #endregion

    }
}
