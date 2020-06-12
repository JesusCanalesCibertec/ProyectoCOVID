using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.constante;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.proceso.dominio;

namespace net.royal.spring.sistema.dao
{

    public interface SyReporteDao : GenericoDao<SyReporte>
    {
        SyReporte obtenerPorId(SyReportePk pk);
        string obtenerImagenComoCadena(string compania, ConstanteReporte.TipoImagen tipoImagen, string periodo, string tipoReporte);
        ParametroPaginacionGenerico listarPorPaginacion(ParametroPaginacionGenerico paginacion, DtoSyReporte filtro);
        SyReporte registrarReporte(UsuarioActual usuarioActual, SyReporte bean);
        SyReporte actualizarReporte(UsuarioActual usuarioActual, SyReporte bean);
        List<SyReporte> listar();
    }

}
