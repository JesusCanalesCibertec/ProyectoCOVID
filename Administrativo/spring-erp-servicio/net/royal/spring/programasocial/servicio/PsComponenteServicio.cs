using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsComponenteServicio : GenericoServicio {
        PsComponente obtenerPorId(PsComponentePk id);
        PsComponente obtenerPorId(String pIdComponente);
        PsComponente coreActualizar(UsuarioActual usuarioActual, PsComponente bean);
        PsComponente coreInsertar(UsuarioActual usuarioActual, PsComponente bean);
        void coreEliminar(PsComponentePk id);
        void coreEliminar(String pIdComponente);
        PsComponente coreAnular(UsuarioActual usuarioActual,PsComponentePk id);
        PsComponente coreAnular(UsuarioActual usuarioActual,String pIdComponente);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroComponente filtro);
        void eliminarfila(string idComponente);
        List<PsComponente> listarTodos();
    }
}
