using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.salud.dominio;
using net.royal.spring.salud.dominio.dto;

namespace net.royal.spring.salud.servicio {

    public interface SsGediagnosticoServicio : GenericoServicio {
        SsGediagnostico obtenerPorId(SsGediagnosticoPk id);
        SsGediagnostico obtenerPorId(int codigo);
        SsGediagnostico coreActualizar(UsuarioActual usuarioActual, SsGediagnostico bean);
        SsGediagnostico coreInsertar(UsuarioActual usuarioActual, SsGediagnostico bean);
        void coreEliminar(SsGediagnosticoPk id);
        void eliminar(Int32? codigo);

        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroDiagnostico filtro);

        SsGediagnostico obtenerNombrePorCodigo(string codigo);
        List<SsGediagnostico> listarActivosFlgCronicos();

        SsGediagnostico coreAnular(UsuarioActual usuarioActual, SsGediagnosticoPk id);
        SsGediagnostico coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdDiagnostico);

    }
}
