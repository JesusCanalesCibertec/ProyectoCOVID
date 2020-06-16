using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsBeneficiarioIngresoDiagnosticoDao : GenericoDao<PsBeneficiarioIngresoDiagnostico>
    {
        PsBeneficiarioIngresoDiagnostico obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso, String pIdDiagnostico);
        PsBeneficiarioIngresoDiagnostico coreActualizar(UsuarioActual usuarioActual, PsBeneficiarioIngresoDiagnostico bean);
        PsBeneficiarioIngresoDiagnostico coreInsertar(UsuarioActual usuarioActual, PsBeneficiarioIngresoDiagnostico bean);
        void coreEliminar(PsBeneficiarioIngresoDiagnosticoPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso, String pIdDiagnostico);
    }
}
