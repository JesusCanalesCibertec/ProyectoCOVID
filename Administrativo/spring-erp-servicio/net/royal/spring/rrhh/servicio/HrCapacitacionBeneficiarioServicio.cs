using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrCapacitacionBeneficiarioServicio : GenericoServicio {
        HrCapacitacionBeneficiario obtenerPorId(HrCapacitacionBeneficiarioPk id);
        HrCapacitacionBeneficiario obtenerPorId(String pCompanyowner,String pCapacitacion,String pIdInstitucion,Nullable<Int32> pIdBeneficiario);
        HrCapacitacionBeneficiario coreActualizar(UsuarioActual usuarioActual, HrCapacitacionBeneficiario bean);
        HrCapacitacionBeneficiario coreInsertar(UsuarioActual usuarioActual, HrCapacitacionBeneficiario bean);
        void coreEliminar(HrCapacitacionBeneficiarioPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion,String pIdInstitucion,Nullable<Int32> pIdBeneficiario);
    }
}
