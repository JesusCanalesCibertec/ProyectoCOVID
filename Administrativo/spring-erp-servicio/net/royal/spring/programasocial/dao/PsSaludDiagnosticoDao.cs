using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsSaludDiagnosticoDao : GenericoDao<PsSaludDiagnostico>
    {
        PsSaludDiagnostico obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiagnostico);
        PsSaludDiagnostico coreActualizar(UsuarioActual usuarioActual, PsSaludDiagnostico bean);
        PsSaludDiagnostico coreInsertar(UsuarioActual usuarioActual, PsSaludDiagnostico bean);
        void coreEliminar(PsSaludDiscapacidadPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiagnostico);

        List<PsSaludDiagnostico> obtenerListado(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
    }
}
