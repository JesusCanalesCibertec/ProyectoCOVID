using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.dto;

namespace net.royal.spring.sistema.dao
{

    public interface SyReportearchivoDao : GenericoDao<SyReporteArchivo>
    {
       // SyReporteArchivo obtenerPorId(SyReportearchivoPk pk);
        ParametroPaginacionGenerico listarPorPaginacion(ParametroPaginacionGenerico paginacion,
             DtoSyReporte filtro);
        SyReporteArchivo registrarReporteArchivo(UsuarioActual usuarioActual, SyReporteArchivo syReportearchivo);
        SyReporteArchivo actualizarReporteArchivo(UsuarioActual usuarioActual, SyReporteArchivo syReportearchivo);
        void eliminarReporteArchivo(SyReportearchivoPk syReportearchivo);
        string obtenerCuerpoCorreo(string reporteIdAplicacion, string reporteId, List<ParametroPersistenciaGenerico> lstMetadata);
    }
}
