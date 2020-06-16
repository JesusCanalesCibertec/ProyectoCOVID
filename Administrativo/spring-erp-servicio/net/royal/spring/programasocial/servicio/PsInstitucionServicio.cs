using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio {

    public interface PsInstitucionServicio : GenericoServicio {
        PsInstitucion obtenerPorId(PsInstitucionPk id);
        PsInstitucion obtenerPorId(string codigo);
        PsInstitucion coreActualizar(UsuarioActual usuarioActual, PsInstitucion bean);
        PsInstitucion coreInsertar(UsuarioActual usuarioActual, PsInstitucion bean);
        void coreEliminar(PsInstitucionPk id);
        void coreEliminar(String pIdInstitucion);
        PsInstitucion coreAnular(UsuarioActual usuarioActual, PsInstitucionPk id);
        PsInstitucion coreAnular(UsuarioActual usuarioActual, String pIdInstitucion);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroInstitucion filtro);
        void eliminar(string codigo);
        List<PsInstitucion> listarTodos();
        List<PsInstitucion> listarTodosActivas();

        List<DtoTabla> listarPeriodos(String Institucion, String IdPrograma, String IdComponente, String IdUsuario, Nullable<Int32> IdBeneficiario);
        String flgSeleccionarInstitucion(UsuarioActual usuarioActual);
    }
}
