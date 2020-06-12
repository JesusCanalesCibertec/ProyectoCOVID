using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.dto;

namespace net.royal.spring.sistema.servicio
{

    public interface SyReportearchivoServicio : GenericoServicio {
        ParametroPaginacionGenerico listarPorPaginacion(ParametroPaginacionGenerico paginacion, DtoSyReporte filtro);
        SyReporteArchivo registrarReporteArchivo(UsuarioActual usuarioActual, SyReporteArchivo syReportearchivo);
        SyReporteArchivo actualizarReporteArchivo(UsuarioActual usuarioActual, SyReporteArchivo syReportearchivo);
        void eliminarReporteArchivo(SyReportearchivoPk syReportearchivo);
        SyReporteArchivo obtenerPorId(SyReportearchivoPk pk);
    }
}
