using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao {

    public interface PsInstitucionDao : GenericoDao<PsInstitucion> {
        PsInstitucion obtenerPorId(string codigo);
        PsInstitucion coreActualizar(UsuarioActual usuarioActual, PsInstitucion bean, String estado);
        PsInstitucion coreInsertar(UsuarioActual usuarioActual, PsInstitucion bean, String estado);
        void coreEliminar(PsInstitucionPk id);
        void coreEliminar(String pIdInstitucion);
        PsInstitucion coreAnular(UsuarioActual usuarioActual, PsInstitucionPk id);
        PsInstitucion coreAnular(UsuarioActual usuarioActual, String pIdInstitucion);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroInstitucion filtro);
        List<PsInstitucion> listarTodosActivas();
        String obtenercadena(string cadena);
        String obtenercodigo(string codigo);
       
        String flgSeleccionarInstitucion(UsuarioActual usuarioActual);
        List<DtoTabla> listarPeriodos(String Institucion, String IdPrograma, String IdComponente, String IdUsuario, Nullable<Int32> IdBeneficiario);
        List<PsInstitucion> listarPorPks(List<string> ids);
        void eliminarAreas(string codigo);
        
    }
}
