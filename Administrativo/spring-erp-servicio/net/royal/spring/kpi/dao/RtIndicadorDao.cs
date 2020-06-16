using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.kpi.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.kpi.dao
{

    public interface RtIndicadorDao : GenericoDao<RtIndicador>
    {
        RtIndicador obtenerPorId(String pIdIndicador);
        RtIndicador coreActualizar(UsuarioActual usuarioActual, RtIndicador bean, String estado);
        RtIndicador coreInsertar(UsuarioActual usuarioActual, RtIndicador bean, String estado);
        void coreEliminar(RtIndicadorPk id);
        void coreEliminar(String pIdIndicador);
        RtIndicador coreAnular(UsuarioActual usuarioActual,RtIndicadorPk id);
        RtIndicador coreAnular(UsuarioActual usuarioActual,String pIdIndicador);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroIndicador filtro);
        String obtenercadena(string cadena);
        String obtenercodigo(string codigo);
        List<RtIndicador> listarPorPrograma(string programa);
    }
}
