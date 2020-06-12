using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrCapacitacionempcalServicio : GenericoServicio {
        HrCapacitacionempcal obtenerPorId(HrCapacitacionempcalPk id);
        HrCapacitacionempcal obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion);
        HrCapacitacionempcal coreActualizar(UsuarioActual usuarioActual, HrCapacitacionempcal bean);
        HrCapacitacionempcal coreInsertar(UsuarioActual usuarioActual, HrCapacitacionempcal bean);
        void coreEliminar(HrCapacitacionempcalPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion);
        HrCapacitacionempcal coreAnular(UsuarioActual usuarioActual,HrCapacitacionempcalPk id);
        HrCapacitacionempcal coreAnular(UsuarioActual usuarioActual,String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion);
    }
}
