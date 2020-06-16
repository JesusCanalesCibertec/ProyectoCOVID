using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio {

    public interface PsSocioAmbientalServicio : GenericoServicio {
        PsSocioAmbiental obtenerPorId(PsSocioAmbientalPk id);
        ParametroPaginacionGenerico listarSocioAmbiental(ParametroPaginacionGenerico paginacion, FiltroSocioAmbiental filtro);
        ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroSocioAmbiental filtro);
        PsSocioAmbiental obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSocioAmbiental);
        PsSocioAmbiental coreActualizar(UsuarioActual usuarioActual, PsSocioAmbiental bean);
        PsSocioAmbiental coreInsertar(UsuarioActual usuarioActual, PsSocioAmbiental bean);
        void coreEliminar(PsSocioAmbientalPk id);
        void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSocioAmbiental);
        PsSocioAmbiental coreAnular(UsuarioActual usuarioActual, PsSocioAmbientalPk id);
        PsSocioAmbiental coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSocioAmbiental);
        string Exportar(FiltroSocioAmbiental lista);
    }
}
