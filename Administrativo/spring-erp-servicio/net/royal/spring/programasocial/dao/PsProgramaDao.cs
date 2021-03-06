using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao
{

    public interface PsProgramaDao : GenericoDao<PsPrograma>
    {
        PsPrograma obtenerPorId(String pIdPrograma);
        PsPrograma coreActualizar(UsuarioActual usuarioActual, PsPrograma bean, String estado);
        PsPrograma coreInsertar(UsuarioActual usuarioActual, PsPrograma bean, String estado);
        void coreEliminar(PsProgramaPk id);
        void coreEliminar(String pIdPrograma);
        PsPrograma coreAnular(UsuarioActual usuarioActual,PsProgramaPk id);
        PsPrograma coreAnular(UsuarioActual usuarioActual,String pIdPrograma);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPrograma filtro);
        List<PsPrograma> listarTodosActivos();
        String obtenercadena(string cadena);
        String obtenercodigo(string codigo);
    }
}
