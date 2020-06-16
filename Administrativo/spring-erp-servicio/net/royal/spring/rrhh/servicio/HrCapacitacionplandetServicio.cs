using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrCapacitacionplandetServicio : GenericoServicio {
        HrCapacitacionplandet obtenerPorId(HrCapacitacionplandetPk id);
        HrCapacitacionplandet obtenerPorId(String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion);
        HrCapacitacionplandet coreActualizar(UsuarioActual usuarioActual, HrCapacitacionplandet bean);
        HrCapacitacionplandet coreInsertar(UsuarioActual usuarioActual, HrCapacitacionplandet bean);
        void coreEliminar(HrCapacitacionplandetPk id);
        void coreEliminar(String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion);
        HrCapacitacionplandet coreAnular(UsuarioActual usuarioActual,HrCapacitacionplandetPk id);
        HrCapacitacionplandet coreAnular(UsuarioActual usuarioActual,String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion);
    }
}
