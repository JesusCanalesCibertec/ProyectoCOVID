using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrCapacitacionplanServicio : GenericoServicio {
        HrCapacitacionplan obtenerPorId(HrCapacitacionplanPk id);
        HrCapacitacionplan obtenerPorId(String pCompanyowner,Nullable<Decimal> pAnioplan);
        HrCapacitacionplan coreActualizar(UsuarioActual usuarioActual, HrCapacitacionplan bean);
        HrCapacitacionplan coreInsertar(UsuarioActual usuarioActual, HrCapacitacionplan bean);
        void coreEliminar(HrCapacitacionplanPk id);
        void coreEliminar(String pCompanyowner,Nullable<Decimal> pAnioplan);
        HrCapacitacionplan coreAnular(UsuarioActual usuarioActual,HrCapacitacionplanPk id);
        HrCapacitacionplan coreAnular(UsuarioActual usuarioActual,String pCompanyowner,Nullable<Decimal> pAnioplan);
    }
}
