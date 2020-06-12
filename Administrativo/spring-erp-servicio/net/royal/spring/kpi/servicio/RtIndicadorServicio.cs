using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.kpi.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.kpi.servicio
{

    public interface RtIndicadorServicio : GenericoServicio {
        RtIndicador obtenerPorId(RtIndicadorPk id);
        RtIndicador obtenerPorId(String pIdIndicador);
        RtIndicador coreActualizar(UsuarioActual usuarioActual, RtIndicador bean);
        RtIndicador coreInsertar(UsuarioActual usuarioActual, RtIndicador bean);
        void coreEliminar(RtIndicadorPk id);
        void coreEliminar(String pIdIndicador);
        RtIndicador coreAnular(UsuarioActual usuarioActual,RtIndicadorPk id);
        RtIndicador coreAnular(UsuarioActual usuarioActual,String pIdIndicador);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroIndicador filtro);
        void eliminarfila(string codigo);
        List<RtIndicador> listarPorPrograma(string programa);
    }
}
