using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrCursodescripcionServicio : GenericoServicio {
        HrCursodescripcion obtenerPorId(HrCursodescripcionPk id);
        HrCursodescripcion obtenerPorId(Nullable<Int32> pCurso);
        HrCursodescripcion coreActualizar(UsuarioActual usuarioActual, HrCursodescripcion bean);
        HrCursodescripcion coreInsertar(UsuarioActual usuarioActual, HrCursodescripcion bean);
        void coreEliminar(HrCursodescripcionPk id);
        void coreEliminar(Nullable<Int32> pCurso);
        HrCursodescripcion coreAnular(UsuarioActual usuarioActual,HrCursodescripcionPk id);
        HrCursodescripcion coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pCurso);

		ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionCurso filtro);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPaginacionCurso filtro);
        HrCursodescripcion registrarCurso(UsuarioActual usuarioActual, HrCursodescripcion bean);
        void eliminarCurso(int curso);
    }
}
