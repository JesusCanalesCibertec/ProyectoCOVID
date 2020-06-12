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
using net.royal.spring.framework.web.controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.programasocial.dominio.dtos;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.constante;

namespace net.royal.spring.programasocial.controladora {

    [Route("api/spring/programasocial/[controller]")]
    public class PsMarcologicoController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsMarcologicoServicio PsMarcologicoServicio;
        private SyPreferencesDao syPreferencesDao;

        public PsMarcologicoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            PsMarcologicoServicio = servicioProveedor.GetService<PsMarcologicoServicio>();
            syPreferencesDao = servicioProveedor.GetService<SyPreferencesDao>();
        }

        [HttpPost("[action]")]
        public PsMarcologico registrar([FromBody]PsMarcologico bean) {
            return PsMarcologicoServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsMarcologico actualizar([FromBody]PsMarcologico bean) {
            return PsMarcologicoServicio.coreActualizar(_usuarioActual, bean);
        }


        [HttpPost("[action]")]
        public PsMarcologico eliminar([FromBody] PsMarcologico bean) {
            PsMarcologicoServicio.eliminar(bean.IdMarcologico);
            return bean;
        }

        [HttpPost("[action]")]
        public PsMarcologico obtenerPorId([FromBody] PsMarcologicoPk llave) {
            return PsMarcologicoServicio.obtenerPorId(llave);
        }

        [HttpPost("[action]")]
        public List<DtoMarcologico> listarTree([FromBody] FiltroMarcologico filtro) {
            return PsMarcologicoServicio.listarTree(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarGraficosListas([FromBody]FiltroGraficos filtro) {
            List<DtoIndicadoresGraficos> listarGraficos = PsMarcologicoServicio.obtenerListaGraficos(_usuarioActual,filtro);
            setSessionData("listarGraficos", listarGraficos);
            return listarGraficos;
        }


        [HttpGet("[action]")]
        public List<DtoGraficoMultiple> MostarGraficos() {
            List<DtoIndicadoresGraficos> listarGraficos = getSessionData<List<DtoIndicadoresGraficos>>("listarGraficos");

            return PsMarcologicoServicio.MostarGraficos(listarGraficos);
        }

        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarGraficosNutricionEstandares([FromBody]FiltroGraficos filtro) {
            List<DtoIndicadoresGraficos> ListaNutricionEstandares = PsMarcologicoServicio.obtenerListaGraficosEstandares(filtro);
            setSessionData("ListaNutricionEstandares", ListaNutricionEstandares);
            return ListaNutricionEstandares;
        }
        [HttpGet("[action]")]
        public List<DtoGraficoMultiple> MostarGraficosNutricionEstandares() {
            List<DtoIndicadoresGraficos> ListaNutricionEstandares = getSessionData<List<DtoIndicadoresGraficos>>("ListaNutricionEstandares");

            return PsMarcologicoServicio.MostrarListaGraficosEstandares(ListaNutricionEstandares);
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> listarBPManufacturada([FromBody]FiltroGraficos filtro) {
            List<ResultadoResGenerico> listarBPManufacturada = PsMarcologicoServicio.listarBPManufacturada(filtro);
            return listarBPManufacturada;
        }
        [HttpGet("[action]")]
        public List<DtoGraficoMultiple> MostarGraficosBPManufacturada(String idPeriodo) {
            return PsMarcologicoServicio.MostarGraficosBPManufacturada(idPeriodo);
        }

        [HttpGet("[action]")]
        public List<Series> MostrarGraficoPie(String idPeriodo, String nombreGrafico, Int32 secuencia, String idPrograma) {
            return PsMarcologicoServicio.MostrarGraficoPie(idPeriodo, nombreGrafico, secuencia, idPrograma, null);
        }

        [HttpGet("[action]")]
        public List<Series> MostrarGraficoPieSexo(String idPeriodo, String nombreGrafico, Int32 secuencia, String idPrograma, String sexo)
        {
            return PsMarcologicoServicio.MostrarGraficoPie(idPeriodo, nombreGrafico, secuencia, idPrograma, sexo);
        }

        [HttpGet("[action]")]
        public List<Series> MostrarGraficoPieSexoEdad(String idPeriodo, String nombreGrafico, Int32 secuencia, String idPrograma, String sexo, Int32 edad)
        {
            return PsMarcologicoServicio.MostrarGraficoPie(idPeriodo, nombreGrafico, secuencia, idPrograma, sexo, edad);
        }


        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccion([FromBody]FiltroGraficos filtro) {
            List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccion = PsMarcologicoServicio.listarReporteNivelSatisfaccion(filtro);
            setSessionData("listarReporteNivelSatisfaccion", listarReporteNivelSatisfaccion);
            return listarReporteNivelSatisfaccion;
        }
        [HttpGet("[action]")]
        public List<DtoGraficoMultiple> MostarGraficosNiveleSatisfacion() {
            List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccion = getSessionData<List<DtoIndicadoresGraficos>>("listarReporteNivelSatisfaccion");
            return PsMarcologicoServicio.MostarGraficos(listarReporteNivelSatisfaccion);
        }


        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccionInstitucion([FromBody]FiltroGraficos filtro) {
            List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccionInstitucion = PsMarcologicoServicio.listarReporteNivelSatisfaccionInstitucion(filtro);
            return listarReporteNivelSatisfaccionInstitucion;
        }

        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReporteSatisfaccionServicioNNA([FromBody]FiltroGraficos filtro) {
            List<DtoIndicadoresGraficos> listarReporteSatisfaccionServicioNNA = PsMarcologicoServicio.listarReporteSatisfaccionServicioNNA(filtro);
            setSessionData("listarReporteSatisfaccionServicioNNA", listarReporteSatisfaccionServicioNNA);
            return listarReporteSatisfaccionServicioNNA;
        }
        [HttpGet("[action]")]
        public List<DtoGraficoMultiple> MostraReporteSatisfaccionServicioNNA() {
            List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccion = getSessionData<List<DtoIndicadoresGraficos>>("listarReporteSatisfaccionServicioNNA");
            return PsMarcologicoServicio.MostarGraficos(listarReporteNivelSatisfaccion);
        }


        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReporteNivelSatisMejorasFisicas([FromBody]FiltroGraficos filtro) {
            List<DtoIndicadoresGraficos> listarReporteNivelSatisMejorasFisicas = PsMarcologicoServicio.listarReporteNivelSatisMejorasFisicas(filtro);
            setSessionData("listarReporteNivelSatisMejorasFisicas", listarReporteNivelSatisMejorasFisicas);
            return listarReporteNivelSatisMejorasFisicas;
        }
        [HttpGet("[action]")]
        public List<DtoGraficoMultiple> MostarReporteNivelSatisMejorasFisicas() {
            List<DtoIndicadoresGraficos> listarReporteNivelSatisMejorasFisicas = getSessionData<List<DtoIndicadoresGraficos>>("listarReporteNivelSatisMejorasFisicas");
            return PsMarcologicoServicio.MostarGraficos(listarReporteNivelSatisMejorasFisicas);
        }


        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarServiciosDeSaludBasicos([FromBody]FiltroGraficos filtro) {
            List<DtoIndicadoresGraficos> listarServiciosDeSaludBasicos = PsMarcologicoServicio.listarServiciosDeSaludBasicos(filtro);
            return listarServiciosDeSaludBasicos;
        }

        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReporteNroNNAParticipan1([FromBody]FiltroGraficos filtro) {
            return PsMarcologicoServicio.listarReporteNroNNAParticipan1(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReporteNroNNAParticipan2([FromBody]FiltroGraficos filtro) {
            return PsMarcologicoServicio.listarReporteNroNNAParticipan2(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReporteEducacionCalidad([FromBody]FiltroGraficos filtro) {
            return PsMarcologicoServicio.listarReporteEducacionCalidad(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReportePorcAAMFortHabSociales([FromBody]FiltroGraficos filtro) {
            return PsMarcologicoServicio.listarReportePorcAAMFortHabSociales(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReportePorcAAMHabOcupac([FromBody]FiltroGraficos filtro) {
            return PsMarcologicoServicio.listarReportePorcAAMHabOcupac(filtro);
        }


        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReportePorcAutonomiaLograda([FromBody]FiltroGraficos filtro) {
            return PsMarcologicoServicio.listarReportePorcAutonomiaLograda(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNA([FromBody]FiltroGraficos filtro) {
            return PsMarcologicoServicio.listarReporteMejoraHabilidadesSocialesNNA(filtro);
        }
        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNAEdad([FromBody]FiltroGraficos filtro) {
            return PsMarcologicoServicio.listarReporteMejoraHabilidadesSocialesNNAEdad(filtro);
        }
        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNASatisf([FromBody]FiltroGraficos filtro) {
            return PsMarcologicoServicio.listarReporteMejoraHabilidadesSocialesNNASatisf(filtro);
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res07Cabecera([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res07Cabecera(filtro); 
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res01Cabecera([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res01Cabecera(filtro); 
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res01Detalle([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res01Detalle(filtro); 
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res19Cabecera([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res19Cabecera(filtro);
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res19Detalle([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res19Detalle(filtro);
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res09Cabecera([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res09Cabecera(filtro);
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res09Detalle([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res09Detalle(filtro);
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res10Cabecera([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res10Cabecera(filtro);
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res11Cabecera([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res11Cabecera(filtro);
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res16Cabecera([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res16Cabecera(filtro);
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res16Detalle([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res16Detalle(filtro);
        }
        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res21Cabecera([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res21Cabecera(filtro);
        }

        [HttpPost("[action]")]
        public List<ResultadoResGenerico> res20Cabecera([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.res20Cabecera(filtro);
        }


        /*ERNESTO INICIO*/
        [HttpPost("[action]")]
        public List<DtoValoresNutricionales> ListarValoresNutricionales([FromBody]FiltroGraficos filtro) {
            return PsMarcologicoServicio.ListarValoresNutricionales(filtro); ;
        }

        [HttpPost("[action]")]
        public List<DtoActividadesPreventivas> ListarActividadesPreventivas([FromBody]FiltroGraficos filtro) {

            return PsMarcologicoServicio.ListarActividadesPreventivas(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoEstadoSalud> ListarEstadoSalud([FromBody]FiltroGraficos filtro) {

            return PsMarcologicoServicio.ListarEstadoSalud(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoAnemiaPrevalente> ListarAnemiaPrevalente([FromBody]FiltroGraficos filtro) {

            return PsMarcologicoServicio.ListarAnemiaPrevalente(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoAccesoEducacion> ListarAccesoEducacion([FromBody]FiltroGraficos filtro) {
            return PsMarcologicoServicio.ListarAccesoEducacion(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoRendimientoEducativo> ListarRendimientoEducativo([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.ListarRendimientoEducativo(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoRetrasoEducativo> ListarRetrasoEducativo([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.ListarRetrasoEducativo(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoRetrasoEducativo> ListarTiempoRetardo([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.ListarTiempoRetardo(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoActividadesPreventivas> ListarPrevalenciaGrupo([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.ListarPrevalenciaGrupo(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoActividadesPreventivas> ListarTratamientoFuera([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.ListarTratamientoFuera(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoEstadoFuncional> ListarEstadoFuncional([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.ListarEstadoFuncional(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoActividadesPreventivas> ListarEstadoFuncionalApoyos([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.ListarEstadoFuncionalApoyos(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoMejoraInfraestructura> ListarMejorasInfraestructura([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.ListarMejorasInfraestructura(filtro);
        }

        [HttpPost("[action]")]
        public List<DtoMejoraProyecto> ListarMejorasProyecto([FromBody]FiltroGraficos filtro)
        {
            return PsMarcologicoServicio.ListarMejorasProyecto(filtro);
        }

        /*ERNESTO FIN*/
        [HttpGet("[action]")]
        public DtoTabla obtenerPeriodoPreferencia()
        {
            DtoTabla periodo = new DtoTabla();
            periodo.Nombre = "2019-12";
            try
            {
                String per = syPreferencesDao.obtenerPorIdCadena(_usuarioActual.UsuarioLogin, ConstanteSeguridad.PREFERENCIA_PERIODO_SOCIAL);
                Console.WriteLine(per);
                if (UString.estaVacio(per))
                {
                    periodo.Nombre = UFechaHora.convertirFechaCadena(DateTime.Today, "yyyy-MM");
                }
                else
                {
                    periodo.Nombre = per.Substring(0, 4) + "-" + per.Substring(4);
                }
            }
            catch (Exception)
            {
                periodo.Nombre = UFechaHora.convertirFechaCadena(DateTime.Today, "yyyy-MM");
            }            
            return periodo;
        }

        [HttpPost("[action]")]
        public List<DtoTabla> obtenerAnalisisNutricionDetalle([FromBody] FiltroEncuestaAnalisis filtro)
        {
            return PsMarcologicoServicio.obtenerAnalisisNutricionDetalle(filtro);
        }
    }
}
