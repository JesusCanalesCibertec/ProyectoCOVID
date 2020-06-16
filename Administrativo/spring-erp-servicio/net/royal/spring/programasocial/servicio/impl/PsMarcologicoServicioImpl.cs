using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Text.RegularExpressions;
using net.royal.spring.programasocial.dominio.dtos;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.constante;
using net.royal.spring.programasocial.constante;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.programasocial.servicio.impl {

    public class PsMarcologicoServicioImpl : GenericoServicioImpl, PsMarcologicoServicio {

        private IServiceProvider servicioProveedor;
        private PsMarcologicoDao PsMarcologicoDao;
        private SyPreferencesDao syPreferencesDao;

        public PsMarcologicoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            PsMarcologicoDao = servicioProveedor.GetService<PsMarcologicoDao>();
            syPreferencesDao = servicioProveedor.GetService<SyPreferencesDao>();
        }

        public PsMarcologico coreInsertar(UsuarioActual usuarioActual, PsMarcologico bean) {
            /*
            string cadena, codigo;
            codigo = PsMarcologicoDao.obtenercodigo(bean.IdInstitucion,bean.IdArea);
            cadena = PsMarcologicoDao.obtenercadena(bean.IdInstitucion,bean.Nombre);

            
            if (codigo != null)
            {
                throw new UException("El código ingresado ya se encuentra registrado");
            }
            if (cadena != null)
            {
                throw new UException("El nombre ingresado ya se encuentra registrado");
            } 
            else
            {
                if (UString.estaVacio(bean.Estado))
                    bean.Estado = ConstanteEstadoGenerico.ACTIVO;
                    bean.IdInstitucion = bean.IdInstitucion.ToUpper();
                return PsMarcologicoDao.coreInsertar(usuarioActual, bean, bean.Estado);
            }
            */
            return PsMarcologicoDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }


        public PsMarcologico coreActualizar(UsuarioActual usuarioActual, PsMarcologico bean) {
            return PsMarcologicoDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }


        public PsMarcologico obtenerPorId(PsMarcologicoPk llave) {
            return PsMarcologicoDao.obtenerPorId(llave.obtenerArreglo());
        }


        public void eliminar(string pIdMarcologico) {

            PsMarcologico objeto = new PsMarcologico();

            objeto.IdMarcologico = pIdMarcologico;


            PsMarcologicoDao.eliminar(objeto);

        }



        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroMarcologico filtro) {
            return PsMarcologicoDao.listarPaginacion(paginacion, filtro);
        }

        public List<DtoMarcologico> listarTree(FiltroMarcologico filtro) {
            return PsMarcologicoDao.listarTree(filtro);
        }

        public List<DtoIndicadoresGraficos> obtenerListaGraficos(UsuarioActual usuarioActual, FiltroGraficos filtro) {
            Console.WriteLine("RES003");
            Console.WriteLine(filtro.IdPeriodo);
            Console.WriteLine(usuarioActual.UsuarioLogin);
            syPreferencesDao.guardarCadena(usuarioActual, usuarioActual.UsuarioLogin, ConstanteSeguridad.PREFERENCIA_PERIODO_SOCIAL, filtro.IdPeriodo, ConstanteProgramaSocial.APLICACION_CODIGO);
            return PsMarcologicoDao.obtenerListaGraficos(filtro);
        }

        public List<DtoGraficoMultiple> MostarGraficos(List<DtoIndicadoresGraficos> lista) {

            List<DtoGraficoMultiple> grafico = new List<DtoGraficoMultiple>();

            for (int g = 0; g < lista.Count; g++) {
                DtoGraficoMultiple cabecera = new DtoGraficoMultiple();
                Series serie = new Series();

                if (g == 0) {
                    if (lista[g].Nnapor != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "% NNA";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Descripcion;
                        serie.value = lista[g].Nnapor.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }
                    if (lista[g].Ncdpor != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "% NCD";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Descripcion;
                        serie.value = lista[g].Ncdpor.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }
                    if (lista[g].Aampor != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "% AAM";

                        serie = new Series();
                        cabecera.series = new List<Series>();
                        serie.name = lista[g].Descripcion;
                        serie.value = lista[g].Aampor.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }

                }

                if (g > 0) {
                    if (lista[g].Descripcion != "TOTAL") {
                        for (int a = 0; a < grafico.Count; a++) {
                            if (grafico[a].name == "% NNA") {
                                serie = new Series();
                                serie.name = lista[g].Descripcion;
                                serie.value = lista[g].Nnapor.Value;
                                grafico[a].series.Add(serie);
                            }
                            if (grafico[a].name == "% NCD") {
                                serie = new Series();
                                serie.name = lista[g].Descripcion;
                                serie.value = lista[g].Ncdpor.Value;
                                grafico[a].series.Add(serie);
                            }
                            if (grafico[a].name == "% AAM") {
                                serie = new Series();
                                serie.name = lista[g].Descripcion;
                                serie.value = lista[g].Aampor.Value;
                                grafico[a].series.Add(serie);
                            }
                        }
                    }


                }
            }

            return grafico;
        }

        public List<DtoIndicadoresGraficos> obtenerListaGraficosEstandares(FiltroGraficos filtro) {
            return PsMarcologicoDao.obtenerListaGraficosEstandares(filtro);
        }

        public List<DtoGraficoMultiple> MostrarListaGraficosEstandares(List<DtoIndicadoresGraficos> lista) {
            List<DtoGraficoMultiple> grafico = new List<DtoGraficoMultiple>();

            for (int g = 0; g < lista.Count; g++) {
                DtoGraficoMultiple cabecera = new DtoGraficoMultiple();
                Series serie = new Series();

                if (g == 0) {
                    if (lista[g].AporFundacionCant != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "% Aporte Fun";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].GruposNutricionales;
                        serie.value = lista[g].AporFundacionPorc.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }
                    if (lista[g].OtrosAportesCant != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "% Aporte Otro";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].GruposNutricionales;
                        serie.value = lista[g].OtrosAportesPorc.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }

                }

                if (g > 0) {
                    if (lista[g].Descripcion != "TOTAL") {
                        for (int a = 0; a < grafico.Count; a++) {
                            if (grafico[a].name == "% Aporte Fun") {
                                serie = new Series();
                                serie.name = lista[g].GruposNutricionales;
                                serie.value = lista[g].AporFundacionPorc.Value;
                                grafico[a].series.Add(serie);
                            }
                            if (grafico[a].name == "% Aporte Otro") {
                                serie = new Series();
                                serie.name = lista[g].GruposNutricionales;
                                serie.value = lista[g].OtrosAportesPorc.Value;
                                grafico[a].series.Add(serie);
                            }
                        }
                    }


                }
            }

            return grafico;
        }

        public List<ResultadoResGenerico> listarBPManufacturada(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarBPManufacturada(filtro);
        }

        public List<DtoGraficoMultiple> MostarGraficosBPManufacturada(String idPeriodo) {
            return PsMarcologicoDao.MostarGraficosBPManufacturada(idPeriodo);
        }

        public List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccion(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReporteNivelSatisfaccion(filtro);
        }

        public List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccionInstitucion(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReporteNivelSatisfaccionInstitucion(filtro);
        }

        public List<DtoValoresNutricionales> ListarValoresNutricionales(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarValoresNutricionales(filtro);
        }

        public List<DtoActividadesPreventivas> ListarActividadesPreventivas(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarActividadesPreventivas(filtro);
        }

        public List<DtoEstadoSalud> ListarEstadoSalud(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarEstadoSalud(filtro);
        }
        public List<DtoIndicadoresGraficos> listarServiciosDeSaludBasicos(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarServiciosDeSaludBasicos(filtro);
        }

        public List<Series> MostrarGraficoPie(string idPeriodo, string NombreGrafico, int secuencia, String idPrograma, String sexo) {
            return PsMarcologicoDao.MostrarGraficoPie(idPeriodo, NombreGrafico, secuencia, idPrograma, sexo, null);
        }
        public List<Series> MostrarGraficoPie(string idPeriodo, string NombreGrafico, int secuencia, String idPrograma, String sexo, Int32 edad)
        {
            return PsMarcologicoDao.MostrarGraficoPie(idPeriodo, NombreGrafico, secuencia, idPrograma, sexo, edad);
        }

        public List<DtoIndicadoresGraficos> listarReporteSatisfaccionServicioNNA(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReporteSatisfaccionServicioNNA(filtro);
        }

        public List<DtoIndicadoresGraficos> listarReporteNroNNAParticipan1(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReporteNroNNAParticipan1(filtro);
        }

        public List<DtoIndicadoresGraficos> listarReporteNroNNAParticipan2(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReporteNroNNAParticipan2(filtro);
        }

        public List<DtoIndicadoresGraficos> listarReporteNivelSatisMejorasFisicas(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReporteNivelSatisMejorasFisicas(filtro);
        }
        public List<DtoAnemiaPrevalente> ListarAnemiaPrevalente(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarAnemiaPrevalente(filtro);
        }

        public List<DtoAccesoEducacion> ListarAccesoEducacion(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarAccesoEducacion(filtro);
        }

        public List<DtoIndicadoresGraficos> listarReporteEducacionCalidad(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReporteEducacionCalidad(filtro);
        }

        public List<DtoIndicadoresGraficos> listarReportePorcAutonomiaLograda(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReportePorcAutonomiaLograda(filtro);
        }

        public List<DtoIndicadoresGraficos> listarReportePorcAAMHabOcupac(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReportePorcAAMHabOcupac(filtro);
        }

        public List<DtoIndicadoresGraficos> listarReportePorcAAMFortHabSociales(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReportePorcAAMFortHabSociales(filtro);
        }

        public List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNA(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReporteMejoraHabilidadesSocialesNNA(filtro);
        }
        public List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNAEdad(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReporteMejoraHabilidadesSocialesNNAEdad(filtro);
        }

        public List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNASatisf(FiltroGraficos filtro) {
            return PsMarcologicoDao.listarReporteMejoraHabilidadesSocialesNNASatisf(filtro);
        }
        public List<DtoRendimientoEducativo> ListarRendimientoEducativo(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarRendimientoEducativo(filtro);
        }

        public List<DtoRetrasoEducativo> ListarRetrasoEducativo(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarRetrasoEducativo(filtro);
        }

        public List<DtoRetrasoEducativo> ListarTiempoRetardo(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarTiempoRetardo(filtro);
        }

        public List<DtoActividadesPreventivas> ListarPrevalenciaGrupo(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarPrevalenciaGrupo(filtro);
        }

        public List<DtoActividadesPreventivas> ListarTratamientoFuera(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarTratamientoFuera(filtro);
        }

        public List<DtoEstadoFuncional> ListarEstadoFuncional(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarEstadoFuncional(filtro);
        }

        public List<DtoActividadesPreventivas> ListarEstadoFuncionalApoyos(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarEstadoFuncionalApoyos(filtro);
        }

        public List<DtoMejoraInfraestructura> ListarMejorasInfraestructura(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarMejorasInfraestructura(filtro);
        }

        public List<DtoMejoraProyecto> ListarMejorasProyecto(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarMejorasProyecto(filtro);
        }



        public List<ResultadoResGenerico> res01Cabecera(FiltroGraficos filtro) {
            return PsMarcologicoDao.res01Cabecera(filtro);
        }

        public List<ResultadoResGenerico> res01Detalle(FiltroGraficos filtro) {
            return PsMarcologicoDao.res01Detalle(filtro);
        }

        public List<ResultadoResGenerico> res07Cabecera(FiltroGraficos filtro) {
            return PsMarcologicoDao.res07Cabecera(filtro);
        }

        public List<ResultadoResGenerico> res09Cabecera(FiltroGraficos filtro) {
            return PsMarcologicoDao.res09Cabecera(filtro);
        }

        public List<ResultadoResGenerico> res09Detalle(FiltroGraficos filtro) {
            return PsMarcologicoDao.res09Detalle(filtro);
        }

        public List<ResultadoResGenerico> res10Cabecera(FiltroGraficos filtro) {
            return PsMarcologicoDao.res10Cabecera(filtro);
        }
        public List<ResultadoResGenerico> res11Cabecera(FiltroGraficos filtro) {
            return PsMarcologicoDao.res11Cabecera(filtro);
        }

        public List<ResultadoResGenerico> res16Cabecera(FiltroGraficos filtro) {
            return PsMarcologicoDao.res16Cabecera(filtro);
        }

        public List<ResultadoResGenerico> res16Detalle(FiltroGraficos filtro) {
            return PsMarcologicoDao.res16Detalle(filtro);
        }

        public List<ResultadoResGenerico> res21Cabecera(FiltroGraficos filtro) {
            return PsMarcologicoDao.res21Cabecera(filtro);
        }

        public List<ResultadoResGenerico> res19Cabecera(FiltroGraficos filtro) {
            return PsMarcologicoDao.res19Cabecera(filtro);
        }

        public List<ResultadoResGenerico> res19Detalle(FiltroGraficos filtro) {
            return PsMarcologicoDao.res19Detalle(filtro);
        }

        public List<ResultadoResGenerico> res20Cabecera(FiltroGraficos filtro) {
            return PsMarcologicoDao.res20Cabecera(filtro);
        }

        public List<DtoReportesGraficos> ListarReportePoblacionPorInstitucionyAnio(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarReportePoblacionPorInstitucionyAnio(filtro);
        }

        public List<DtoGraficoMultiple> MostarReportePoblacionPorInstitucionyAnio(List<DtoReportesGraficos> lista) {
            List<DtoGraficoMultiple> grafico = new List<DtoGraficoMultiple>();

            for (int g = 0; g < lista.Count; g++) {
                DtoGraficoMultiple cabecera = new DtoGraficoMultiple();
                Series serie = new Series();

                if (g == 0) {
                    if (lista[g].Puricultorio != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "PUERICULTORIO PÉREZ ARANIBAR";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Anio.ToString();
                        serie.value = lista[g].Puricultorio.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }


                    if (lista[g].Sanvicente != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "CARGG SAN VICENTE DE PAUL";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Anio.ToString();
                        serie.value = lista[g].Sanvicente.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }

                    if (lista[g].Canevaro != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "CARGG IGNACIA R. VDA.DE CANEVARO";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Anio.ToString();
                        serie.value = lista[g].Canevaro.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }
                    if (lista[g].Desamparados != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "HERMANAS DE LOS ANCIANOS DESAMPARADOS";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Anio.ToString();
                        serie.value = lista[g].Desamparados.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }


                    if (lista[g].Misioneras != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "MISIONERAS DE LA CARIDAD MADRE TERESA DE CALCUTA";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Anio.ToString();
                        serie.value = lista[g].Misioneras.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }
                    if (lista[g].Inmaculada != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "CEBE 07 LA INMACULADA";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Anio.ToString();
                        serie.value = lista[g].Inmaculada.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }
                    if (lista[g].Sanfrancisco != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "CEBE 09 SAN FRANCISCO DE ASIS";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Anio.ToString();
                        serie.value = lista[g].Sanfrancisco.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }

                    if (lista[g].Total != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "TOTAL";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Anio.ToString();
                        serie.value = lista[g].Total.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }

                }

                if (g > 0) {

                    for (int a = 0; a < grafico.Count; a++) {
                        if (grafico[a].name == "PUERICULTORIO PÉREZ ARANIBAR") {
                            serie = new Series();
                            serie.name = lista[g].Anio.ToString();
                            serie.value = lista[g].Puricultorio.Value;
                            grafico[a].series.Add(serie);
                        }
                        if (grafico[a].name == "CARGG SAN VICENTE DE PAUL") {
                            serie = new Series();
                            serie.name = lista[g].Anio.ToString();
                            serie.value = lista[g].Sanvicente.Value;
                            grafico[a].series.Add(serie);
                        }

                        if (grafico[a].name == "CARGG IGNACIA R. VDA.DE CANEVARO") {

                            serie = new Series();
                            serie.name = lista[g].Anio.ToString();
                            serie.value = lista[g].Canevaro.Value;
                            grafico[a].series.Add(serie);
                        }
                        if (grafico[a].name == "HERMANAS DE LOS ANCIANOS DESAMPARADOS") {

                            serie = new Series();
                            serie.name = lista[g].Anio.ToString();
                            serie.value = lista[g].Desamparados.Value;
                            grafico[a].series.Add(serie);
                        }
                        if (grafico[a].name == "MISIONERAS DE LA CARIDAD MADRE TERESA DE CALCUTA") {
                            serie = new Series();
                            serie.name = lista[g].Anio.ToString();
                            serie.value = lista[g].Misioneras.Value;
                            grafico[a].series.Add(serie);
                        }
                        if (grafico[a].name == "CEBE 07 LA INMACULADA") {

                            serie = new Series();
                            serie.name = lista[g].Anio.ToString();
                            serie.value = lista[g].Inmaculada.Value;
                            grafico[a].series.Add(serie);
                        }
                        if (grafico[a].name == "CEBE 09 SAN FRANCISCO DE ASIS") {

                            serie = new Series();
                            serie.name = lista[g].Anio.ToString();
                            serie.value = lista[g].Sanfrancisco.Value;
                            grafico[a].series.Add(serie);
                        }

                        if (grafico[a].name == "TOTAL") {

                            serie = new Series();
                            serie.name = lista[g].Anio.ToString();
                            serie.value = lista[g].Total.Value;
                            grafico[a].series.Add(serie);
                        }

                    }
                }
            }

            return grafico;
        }

        public List<DtoReportesGraficos> ListarReportePoblacionBeneficiaria(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarReportePoblacionBeneficiaria(filtro);
        }

        public List<DtoGraficoMultiple> MostarReportePoblacionBeneficiaria(List<DtoReportesGraficos> lista) {


            List<DtoGraficoMultiple> grafico = new List<DtoGraficoMultiple>();

            for (int g = 0; g < lista.Count; g++) {

                DtoGraficoMultiple cabecera = new DtoGraficoMultiple();
                Series serie = new Series();

                if (g == 0) {
                    if (lista[g].Anio != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "Años";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Anio.ToString();
                        serie.value = lista[g].Anio.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }


                    if (lista[g].Total != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "Población";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Anio.ToString();
                        serie.value = lista[g].Total.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }
                }


                if (g > 0) {

                    for (int a = 0; a < grafico.Count; a++) {
                        if (grafico[a].name == "Años") {
                            serie = new Series();
                            serie.name = lista[g].Anio.ToString();
                            serie.value = lista[g].Anio.Value;
                            grafico[a].series.Add(serie);
                        }
                        if (grafico[a].name == "Población") {
                            serie = new Series();
                            serie.name = lista[g].Anio.ToString();
                            serie.value = lista[g].Total.Value;
                            grafico[a].series.Add(serie);
                        }
                    }
                }

            }

            return grafico;

        }

        public List<DtoReportesGraficos> ListarReporteSubvencionesPorPrograma(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarReporteSubvencionesPorPrograma(filtro);
        }

        public List<DtoGraficoMultiple> MostarListarReporteSubvencionesPorPrograma(List<DtoReportesGraficos> lista) {
            List<DtoGraficoMultiple> grafico = new List<DtoGraficoMultiple>();

            for (int g = 0; g < lista.Count; g++) {
                DtoGraficoMultiple cabecera = new DtoGraficoMultiple();
                Series serie = new Series();

                if (g == 0) {
                    if (lista[g].PuricultorioD != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "Puricultorio";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Tipo.ToString();
                        serie.value = lista[g].PuricultorioD.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }


                    if (lista[g].CanevaroD != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "Canevaro";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Tipo.ToString();
                        serie.value = lista[g].CanevaroD.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }
                    if (lista[g].SanvicenteD != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "San Vicente";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Tipo.ToString();
                        serie.value = lista[g].SanvicenteD.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }



                    if (lista[g].InmaculadaD != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "Inmaculada";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Tipo.ToString();
                        serie.value = lista[g].InmaculadaD.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }
                    if (lista[g].SanfranciscoD != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "San francisco";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Tipo.ToString();
                        serie.value = lista[g].SanfranciscoD.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }
                    if (lista[g].AsiloHermanitas != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "AsiloHermanitas";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Tipo.ToString();
                        serie.value = lista[g].AsiloHermanitas.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }
                    if (lista[g].MisionerasD != null) {
                        cabecera = new DtoGraficoMultiple();
                        cabecera.name = "Misioneras";

                        cabecera.series = new List<Series>();
                        serie = new Series();
                        serie.name = lista[g].Tipo.ToString();
                        serie.value = lista[g].MisionerasD.Value;
                        cabecera.series.Add(serie);
                        grafico.Add(cabecera);
                    }

                }

                if (g > 0) {

                    for (int a = 0; a < grafico.Count; a++) {
                        if (grafico[a].name == "Puricultorio") {
                            serie = new Series();
                            serie.name = lista[g].Tipo.ToString();
                            serie.value = lista[g].PuricultorioD.Value;
                            grafico[a].series.Add(serie);
                        }

                        if (grafico[a].name == "Canevaro") {

                            serie = new Series();
                            serie.name = lista[g].Tipo.ToString();
                            serie.value = lista[g].CanevaroD.Value;
                            grafico[a].series.Add(serie);
                        }

                        if (grafico[a].name == "San Vicente") {
                            serie = new Series();
                            serie.name = lista[g].Tipo.ToString();
                            serie.value = lista[g].SanvicenteD.Value;
                            grafico[a].series.Add(serie);
                        }

                        if (grafico[a].name == "Inmaculada") {

                            serie = new Series();
                            serie.name = lista[g].Tipo.ToString();
                            serie.value = lista[g].InmaculadaD.Value;
                            grafico[a].series.Add(serie);
                        }

                        if (grafico[a].name == "San francisco") {

                            serie = new Series();
                            serie.name = lista[g].Tipo.ToString();
                            serie.value = lista[g].SanfranciscoD.Value;
                            grafico[a].series.Add(serie);
                        }

                        if (grafico[a].name == "AsiloHermanitas") {

                            serie = new Series();
                            serie.name = lista[g].Tipo.ToString();
                            serie.value = lista[g].AsiloHermanitas.Value;
                            grafico[a].series.Add(serie);
                        }

                        if (grafico[a].name == "Misioneras") {
                            serie = new Series();
                            serie.name = lista[g].Tipo.ToString();
                            serie.value = lista[g].MisionerasD.Value;
                            grafico[a].series.Add(serie);
                        }
                    }
                }
            }

            return grafico;
        }

        public List<DtoReportesGraficos> ListarReporteSubvencionesPorInstitucion(FiltroGraficos filtro) {
            return PsMarcologicoDao.ListarReporteSubvencionesPorInstitucion(filtro);
        }

        public List<DtoGraficoMultiple> MostarReporteSubvencionesPorInstitucion(List<DtoReportesGraficos> lista) {
            /*List<Series> series = new List<Series>();
            for (int g = 0; g < lista.Count; g++) {

                Series serie = new Series();

                if (g == 0) {
                    if (lista[g].PuricultorioD != null) {
                        serie = new Series();
                        serie.name = "PUERICULTORIO PÉREZ ARANIBAR";
                        serie.value = lista[g].PuricultorioD.Value;
                        series.Add(serie);
                    }


                    if (lista[g].CanevaroD != null) {

                        serie = new Series();
                        serie.name = "CARGG IGNACIA R.VDA.DE CANEVARO";
                        serie.value = lista[g].CanevaroD.Value;

                        series.Add(serie);
                    }
                    if (lista[g].SanvicenteD != null) {

                        serie = new Series();
                        serie.name = "CARGG SAN VICENTE DE PAUL";
                        serie.value = lista[g].SanvicenteD.Value;

                        series.Add(serie);
                    }








                    if (lista[g].InmaculadaD != null) {

                        serie = new Series();
                        serie.name = "CEBE 07 LA INMACULADA";
                        serie.value = lista[g].InmaculadaD.Value;

                        series.Add(serie);
                    }
                    if (lista[g].SanfranciscoD != null) {

                        serie = new Series();
                        serie.name = "CEBE 09 SAN FRANCISCO DE ASIS";
                        serie.value = lista[g].SanfranciscoD.Value;

                        series.Add(serie);
                    }
                    if (lista[g].AsiloHermanitas != null) {

                        serie = new Series();
                        serie.name = "HERMANAS DE LOS ANCIANOS DESAMPARADOS";
                        serie.value = lista[g].AsiloHermanitas.Value;

                        series.Add(serie);
                    }
                    if (lista[g].MisionerasD != null) {

                        serie = new Series();
                        serie.name = "MISIONERAS DE LA CARIDAD MADRE TERESA DE CALCUTA";
                        serie.value = lista[g].MisionerasD.Value;

                        series.Add(serie);
                    }

                }
            }

            return series;*/
            
            return MostarListarReporteSubvencionesPorPrograma(lista);
        }

        public List<DtoTabla> obtenerAnalisisNutricionDetalle(FiltroEncuestaAnalisis filtro)
        {
            return PsMarcologicoDao.obtenerAnalisisNutricionDetalle(filtro);
        }
    }
}


