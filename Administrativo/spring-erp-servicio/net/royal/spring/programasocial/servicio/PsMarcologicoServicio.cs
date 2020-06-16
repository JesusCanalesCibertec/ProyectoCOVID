using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.programasocial.dominio.dtos;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio {

    public interface PsMarcologicoServicio : GenericoServicio {

        PsMarcologico obtenerPorId(PsMarcologicoPk llave);
        PsMarcologico coreActualizar(UsuarioActual usuarioActual, PsMarcologico bean);
        PsMarcologico coreInsertar(UsuarioActual usuarioActual, PsMarcologico bean);
        void eliminar(string pIdMarcologico);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroMarcologico filtro);
        List<DtoMarcologico> listarTree(FiltroMarcologico filtro);
        List<DtoIndicadoresGraficos> obtenerListaGraficos(UsuarioActual usuarioActual, FiltroGraficos filtro);
        List<DtoGraficoMultiple> MostarGraficos(List<DtoIndicadoresGraficos> lista);

        List<DtoIndicadoresGraficos> obtenerListaGraficosEstandares(FiltroGraficos filtro);
        List<ResultadoResGenerico> listarBPManufacturada(FiltroGraficos filtro);

        List<DtoGraficoMultiple> MostrarListaGraficosEstandares(List<DtoIndicadoresGraficos> lista);
        List<DtoGraficoMultiple> MostarGraficosBPManufacturada(String idPeriodo);
        List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccion(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccionInstitucion(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReporteSatisfaccionServicioNNA(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReporteNivelSatisMejorasFisicas(FiltroGraficos filtro);
        List<Series> MostrarGraficoPie(String idPeriodo, String NombreGrafico, Int32 secuencia, String idPrograma, String sexo);
        List<Series> MostrarGraficoPie(String idPeriodo, String NombreGrafico, Int32 secuencia, String idPrograma, String sexo, Int32 edad);

        List<DtoValoresNutricionales> ListarValoresNutricionales(FiltroGraficos filtro);
        List<DtoActividadesPreventivas> ListarActividadesPreventivas(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarServiciosDeSaludBasicos(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReporteNroNNAParticipan1(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReporteNroNNAParticipan2(FiltroGraficos filtro);
        List<DtoAnemiaPrevalente> ListarAnemiaPrevalente(FiltroGraficos filtro);
        List<DtoAccesoEducacion> ListarAccesoEducacion(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReportePorcAutonomiaLograda(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReportePorcAAMHabOcupac(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReportePorcAAMFortHabSociales(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReporteEducacionCalidad(FiltroGraficos filtro);
        List<DtoEstadoSalud> ListarEstadoSalud(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNA(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNAEdad(FiltroGraficos filtro);
        List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNASatisf(FiltroGraficos filtro);
        List<DtoRendimientoEducativo> ListarRendimientoEducativo(FiltroGraficos filtro);
        List<DtoRetrasoEducativo> ListarRetrasoEducativo(FiltroGraficos filtro);
        List<DtoRetrasoEducativo> ListarTiempoRetardo(FiltroGraficos filtro);
        List<DtoActividadesPreventivas> ListarPrevalenciaGrupo(FiltroGraficos filtro);
        List<DtoActividadesPreventivas> ListarTratamientoFuera(FiltroGraficos filtro);
        List<DtoEstadoFuncional> ListarEstadoFuncional(FiltroGraficos filtro);
        List<DtoActividadesPreventivas> ListarEstadoFuncionalApoyos(FiltroGraficos filtro);
        List<DtoMejoraInfraestructura> ListarMejorasInfraestructura(FiltroGraficos filtro);
        List<DtoMejoraProyecto> ListarMejorasProyecto(FiltroGraficos filtro);
        List<ResultadoResGenerico> res01Cabecera(FiltroGraficos filtro);
        List<ResultadoResGenerico> res01Detalle(FiltroGraficos filtro);
        List<ResultadoResGenerico> res07Cabecera(FiltroGraficos filtro);
        List<ResultadoResGenerico> res09Cabecera(FiltroGraficos filtro);
        List<ResultadoResGenerico> res09Detalle(FiltroGraficos filtro);
        List<ResultadoResGenerico> res10Cabecera(FiltroGraficos filtro);
        List<ResultadoResGenerico> res11Cabecera(FiltroGraficos filtro);
        List<ResultadoResGenerico> res16Cabecera(FiltroGraficos filtro);
        List<ResultadoResGenerico> res16Detalle(FiltroGraficos filtro);
        List<ResultadoResGenerico> res21Cabecera(FiltroGraficos filtro);
        List<ResultadoResGenerico> res19Cabecera(FiltroGraficos filtro);
        List<ResultadoResGenerico> res19Detalle(FiltroGraficos filtro);
        List<ResultadoResGenerico> res20Cabecera(FiltroGraficos filtro);


        List<DtoReportesGraficos> ListarReportePoblacionPorInstitucionyAnio(FiltroGraficos filtro);
        List<DtoGraficoMultiple> MostarReportePoblacionPorInstitucionyAnio(List<DtoReportesGraficos> lista);

        List<DtoReportesGraficos> ListarReportePoblacionBeneficiaria(FiltroGraficos filtro);
        List<DtoGraficoMultiple> MostarReportePoblacionBeneficiaria(List<DtoReportesGraficos> lista);

        List<DtoReportesGraficos> ListarReporteSubvencionesPorPrograma(FiltroGraficos filtro);
        List<DtoGraficoMultiple> MostarListarReporteSubvencionesPorPrograma(List<DtoReportesGraficos> lista);

        List<DtoReportesGraficos> ListarReporteSubvencionesPorInstitucion(FiltroGraficos filtro);
        List<DtoGraficoMultiple> MostarReporteSubvencionesPorInstitucion(List<DtoReportesGraficos> lista);
        List<DtoTabla> obtenerAnalisisNutricionDetalle(FiltroEncuestaAnalisis filtro);
    }
}