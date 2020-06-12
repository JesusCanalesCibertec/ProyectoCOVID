using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.correo.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.sistema.constante;
using net.royal.spring.proceso.dominio;

namespace net.royal.spring.sistema.servicio
{

    public interface SyReporteServicio : GenericoServicio
    {
        String ejecutarReporteComoUrlTemporal(UsuarioActual usuarioctual, DtoReporteParametro reporte, List<ParametroPersistenciaGenerico> lstParametros);

        byte[] ejecutarReporte(DtoReporteParametro reporte, List<ParametroPersistenciaGenerico> lstParametros);
        List<Email> generarListaCorreo(DtoReporteParametro reporte, List<ParametroPersistenciaGenerico> lstParametros, List<DtoCorreo> listaCorreos);

        string obtenerImagenComoCadena(string compania, ConstanteReporte.TipoImagen tipoImagen, string periodo, string tipoRecurso);
        ParametroPaginacionGenerico listarPorPaginacion(ParametroPaginacionGenerico paginacion,
             DtoSyReporte filtro);
        SyReporte registrarReporte(UsuarioActual usuarioActual, SyReporte bean);
        SyReporte obtenerPorId(SyReportePk pk);
        SyReporte actualizarReporte(UsuarioActual usuarioActual, SyReporte bean);
        SyReporte actualizar(SyReporte recurso);
        string crearReporteUrl(string ruta, Byte[] archivo);
        string[] obtenerTextoReporte(SyReportearchivoPk pk);
        List<SyReporte> listar();
    }
}


