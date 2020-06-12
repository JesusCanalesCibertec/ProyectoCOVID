using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao
{

    public interface PsSocioAmbientalDao : GenericoDao<PsSocioAmbiental>
    {
        PsSocioAmbiental obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSocioAmbiental);
        PsSocioAmbiental coreActualizar(UsuarioActual usuarioActual, PsSocioAmbiental bean, String estado);
        PsSocioAmbiental coreInsertar(UsuarioActual usuarioActual, PsSocioAmbiental bean, String estado);
        void coreEliminar(PsSocioAmbientalPk id);
        ParametroPaginacionGenerico listarSocioAmbiental(ParametroPaginacionGenerico paginacion, FiltroSocioAmbiental filtro);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSocioAmbiental);
        PsSocioAmbiental coreAnular(UsuarioActual usuarioActual,PsSocioAmbientalPk id);
        PsSocioAmbiental coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSocioAmbiental);

        ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroSocioAmbiental filtro);
    }
}
