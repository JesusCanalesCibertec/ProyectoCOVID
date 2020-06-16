using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.programasocial.dominio.dtos;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio {

    public interface PsSaludServicio : GenericoServicio {

        DtoPsSalud obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
        PsSalud coreActualizar(UsuarioActual usuarioActual, DtoPsSalud bean);
        PsSalud coreInsertar(UsuarioActual usuarioActual, DtoPsSalud bean);
        DtoTabla calcularHemoglobina(DtoPsSalud bean);
        void coreEliminar(PsSaludPk id);
        void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
        PsSalud coreAnular(UsuarioActual usuarioActual, PsSaludPk id);
        PsSalud coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
        ParametroPaginacionGenerico listarSaludPaginacion(ParametroPaginacionGenerico paginacion, FiltroSalud filtro);
        DtoPsSalud obtenerPorListados(String IdInstitucion, int? IdBeneficiario, int? IdSalud);
        List<DtoPsSalud> listarDiagnosticos(String IdInstitucion, int? IdBeneficiario, String IdTipoRas);
        List<DtoPsSalud> listarTratamientos(String IdInstitucion, int? IdBeneficiario, int? IdDetalle, int? IdDiagnostico);
        ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroSalud filtro);
        string Exportar(FiltroSalud filtro);
    }
}
