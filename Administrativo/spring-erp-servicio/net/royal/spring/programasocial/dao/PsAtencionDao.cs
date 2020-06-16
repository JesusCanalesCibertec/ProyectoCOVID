using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.programasocial.dominio.dtos;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao {

    public interface PsAtencionDao : GenericoDao<PsAtencion> {
        PsAtencion obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdAtencion);
        PsAtencion coreActualizar(UsuarioActual usuarioActual, DtoAtencion bean);
        PsAtencion coreInsertar(UsuarioActual usuarioActual, DtoAtencion bean);
        void coreEliminar(PsAtencionPk id);
        void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdAtencion);
        PsAtencion coreAnular(UsuarioActual usuarioActual, PsAtencionPk id);
        PsAtencion coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdAtencion);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroRas filtro);

        ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroRas filtro);
    }
}
