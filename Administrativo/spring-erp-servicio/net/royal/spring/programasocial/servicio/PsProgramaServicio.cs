using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsProgramaServicio : GenericoServicio {
        PsPrograma obtenerPorId(PsProgramaPk id);
        PsPrograma obtenerPorId(String pIdPrograma);
        PsPrograma coreActualizar(UsuarioActual usuarioActual, PsPrograma bean);
        PsPrograma coreInsertar(UsuarioActual usuarioActual, PsPrograma bean);
        void coreEliminar(PsProgramaPk id);
        void coreEliminar(String pIdPrograma);
        PsPrograma coreAnular(UsuarioActual usuarioActual,PsProgramaPk id);
        PsPrograma coreAnular(UsuarioActual usuarioActual,String pIdPrograma);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPrograma filtro);
        void eliminar(string idPrograma);
        List<PsPrograma> listarTodos();
        List<PsPrograma> listarTodosActivos();
        List<PsPrograma> listarPorInstitucion(String pIdUsuaio, String pIdInstitucion);
        List<PsInstitucion> listarPorPrograma(string pIdPrograma);
    }
}
