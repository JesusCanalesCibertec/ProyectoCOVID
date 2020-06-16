using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.dao
{

    public interface HrCursodescripcionDao : GenericoDao<HrCursodescripcion>
    {
        HrCursodescripcion obtenerPorId(Nullable<Int32> pCurso);
        HrCursodescripcion coreActualizar(UsuarioActual usuarioActual, HrCursodescripcion bean, String estado);
        HrCursodescripcion coreInsertar(UsuarioActual usuarioActual, HrCursodescripcion bean, String estado);
        void coreEliminar(HrCursodescripcionPk id);
        void coreEliminar(Nullable<Int32> pCurso);
        HrCursodescripcion coreAnular(UsuarioActual usuarioActual,HrCursodescripcionPk id);
        HrCursodescripcion coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pCurso);
		ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionCurso filtro);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPaginacionCurso filtro);
        HrCursodescripcion registrarCurso(UsuarioActual usuarioActual, HrCursodescripcion bean);
        String obtenerDescripcion(string nombre);
    }
}
