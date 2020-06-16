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

    public interface PsCapacidadDao : GenericoDao<PsCapacidad> {
        DtoCapacidad obtenerPorId(PsCapacidadPk id);
        PsCapacidad coreActualizar(UsuarioActual usuarioActual, DtoCapacidad bean, String estado);
        PsCapacidad coreInsertar(UsuarioActual usuarioActual, DtoCapacidad bean, String estado);
        DtoCapacidad calcularAniosRetraso(DtoCapacidad bean);
        void coreEliminar(PsCapacidadPk id);
        void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad);
        PsCapacidad coreAnular(UsuarioActual usuarioActual, PsCapacidadPk id);
        PsCapacidad coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad);
        ParametroPaginacionGenerico listarCapacidades(ParametroPaginacionGenerico paginacion, FiltroCapacidad filtro);
        DtoCapacidad calcularBarthel(DtoCapacidad bean);
        DtoCapacidad calcularKatz(DtoCapacidad bean);
        ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroCapacidad filtro);
    }
}
