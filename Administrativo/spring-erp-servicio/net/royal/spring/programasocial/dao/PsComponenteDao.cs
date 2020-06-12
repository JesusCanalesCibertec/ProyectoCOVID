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

    public interface PsComponenteDao : GenericoDao<PsComponente>
    {
        PsComponente obtenerPorId(String pIdComponente);
        PsComponente coreActualizar(UsuarioActual usuarioActual, PsComponente bean, String estado);
        PsComponente coreInsertar(UsuarioActual usuarioActual, PsComponente bean, String estado);
        void coreEliminar(PsComponentePk id);
        void coreEliminar(String pIdComponente);
        PsComponente coreAnular(UsuarioActual usuarioActual,PsComponentePk id);
        PsComponente coreAnular(UsuarioActual usuarioActual,String pIdComponente);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroComponente filtro);
        String obtenercadena(string cadena);
        String obtenercodigo(string codigo);
    }
}
