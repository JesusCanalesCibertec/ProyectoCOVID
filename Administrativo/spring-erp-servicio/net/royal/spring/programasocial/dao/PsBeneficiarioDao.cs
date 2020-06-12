using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao
{

    public interface PsBeneficiarioDao : GenericoDao<PsBeneficiario>
    {
        PsBeneficiario obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario);
        PsBeneficiario coreActualizar(UsuarioActual usuarioActual, PsBeneficiario bean, String estado);
        PsBeneficiario coreInsertar(UsuarioActual usuarioActual, PsBeneficiario bean, String estado);
        void coreEliminar(PsBeneficiarioPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario);
        PsBeneficiario coreAnular(UsuarioActual usuarioActual,PsBeneficiarioPk id);
        PsBeneficiario coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroBeneficiario filtro);
        void eliminarDetalles(string auxInstitucion, int? idEntidad, int? idUltimoIngreso);
        List<MaMiscelaneosdetalle> listarValoracionNutricional(Int32 IdBeneficiario, String IdInstitucion);
		List<string> listarCorreos(string auxInstitucion, string v);
		ParametroPaginacionGenerico listarBeneficiarios(ParametroPaginacionGenerico paginacion, FiltroBeneficiario filtro);

        int generarCodigo(string auxInstitucion);

        DtoPsBeneficiario obtenerPrograma(PsEntidad bean);
        List<PsBeneficiario> obtenerPorEntidad(int? idEntidad);
        List<DtoPrevencionSalud> ListarBeneficiariosSinEvaluacion(FiltroGraficos filtro);
		ParametroPaginacionGenerico listarPaginacion2(ParametroPaginacionGenerico paginacion, FiltroBeneficiario filtro);
		ParametroPaginacionGenerico paginacionBeneficiariosSinEvaluacion(ParametroPaginacionGenerico paginacion, FiltroGraficos filtro);
    }
}
