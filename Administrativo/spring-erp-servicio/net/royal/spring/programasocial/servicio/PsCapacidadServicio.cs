using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.programasocial.dominio.dtos;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio {

    public interface PsCapacidadServicio : GenericoServicio {
        DtoCapacidad obtenerPorId(PsCapacidadPk id);
        PsCapacidad obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad);
        PsCapacidad coreActualizar(UsuarioActual usuarioActual, DtoCapacidad bean);
        PsCapacidad coreInsertar(UsuarioActual usuarioActual, DtoCapacidad bean);
        void coreEliminar(PsCapacidadPk id);
        void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad);
        PsCapacidad coreAnular(UsuarioActual usuarioActual, PsCapacidadPk id);
        DtoCapacidad calcularAniosRetraso(DtoCapacidad bean);
        PsCapacidad coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad);

        ParametroPaginacionGenerico listarCapacidades(ParametroPaginacionGenerico paginacion, FiltroCapacidad filtro);
        ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroCapacidad filtro);

        DtoCapacidad calcularBarthel(DtoCapacidad bean);
        DtoCapacidad calcularKatz(DtoCapacidad bean);
        string Exportar(FiltroCapacidad filtro);
    }
}
