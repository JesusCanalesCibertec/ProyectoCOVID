using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrCapacitacionBeneficiarioDao : GenericoDao<HrCapacitacionBeneficiario>
    {
        HrCapacitacionBeneficiario obtenerPorId(String pCompanyowner,String pCapacitacion,String pIdInstitucion,Nullable<Int32> pIdBeneficiario);
        HrCapacitacionBeneficiario coreActualizar(UsuarioActual usuarioActual, HrCapacitacionBeneficiario bean);
        HrCapacitacionBeneficiario coreInsertar(UsuarioActual usuarioActual, HrCapacitacionBeneficiario bean);
        void coreEliminar(HrCapacitacionBeneficiarioPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion,String pIdInstitucion,Nullable<Int32> pIdBeneficiario);
        List<HrCapacitacionBeneficiario> listarPorCapacitacion(HrCapacitacionPk hrCapacitacionPk);
    }
}
