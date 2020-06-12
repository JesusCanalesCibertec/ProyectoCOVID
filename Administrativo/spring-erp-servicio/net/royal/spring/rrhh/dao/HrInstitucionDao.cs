using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.dao
{

    public interface HrInstitucionDao : GenericoDao<PsInstitucion>
    {
        PsInstitucion obtenerPorId(string codigo);
        PsInstitucion coreActualizar(UsuarioActual usuarioActual, PsInstitucion bean, String estado);
        PsInstitucion coreInsertar(UsuarioActual usuarioActual, PsInstitucion bean, String estado);
        void coreEliminar(PsInstitucion id);
        void coreEliminar(string codigo);
        PsInstitucion coreAnular(UsuarioActual usuarioActual, PsInstitucionPk id);
        PsInstitucion coreAnular(UsuarioActual usuarioActual,string codigo);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPaginacionCurso filtro);
        PsInstitucion registrarCurso(UsuarioActual usuarioActual, PsInstitucion bean);
        String obtenerDescripcion(string nombre);
    }
}
