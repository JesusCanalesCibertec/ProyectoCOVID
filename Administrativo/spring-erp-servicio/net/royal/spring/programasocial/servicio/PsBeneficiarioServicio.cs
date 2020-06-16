using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsBeneficiarioServicio : GenericoServicio {
        PsBeneficiario obtenerPorId(PsBeneficiarioPk id);
        PsBeneficiario obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario);
        PsBeneficiario coreActualizar(UsuarioActual usuarioActual, PsBeneficiario bean);
        PsBeneficiario coreInsertar(UsuarioActual usuarioActual, PsBeneficiario bean);
        void coreEliminar(PsBeneficiarioPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario);
        PsBeneficiario coreAnular(UsuarioActual usuarioActual,PsBeneficiarioPk id);
        PsBeneficiario coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroBeneficiario filtro);

        string Exportar(FiltroBeneficiario filtro);
        PsEntidad registrarCompleto(UsuarioActual usuarioActual, PsEntidad bean);
        PsEntidad actualizarCompleto(UsuarioActual usuarioActual, PsEntidad bean);
        PsEntidad obtenerCompleto(string institucion, Int32 idBeneficiario);
        List<MaMiscelaneosdetalle> listarValoracionNutricional(Int32 idInstitucion, String IdInstitucion);
        ParametroPaginacionGenerico listarBeneficiarios(ParametroPaginacionGenerico paginacion, FiltroBeneficiario filtro);
        DtoBeneficiario anularBeneficiario(DtoBeneficiario bean, UsuarioActual usuarioActual);
        PsEntidad obtenerDatosBasicos(PsEntidad bean);
        List<DtoPrevencionSalud> ListarBeneficiariosSinEvaluacion(FiltroGraficos filtro);
        ParametroPaginacionGenerico paginacionBeneficiariosSinEvaluacion(ParametroPaginacionGenerico paginacion, FiltroGraficos filtro);
    }
}
