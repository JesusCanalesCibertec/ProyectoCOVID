using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrCapacitacionempcalDao : GenericoDao<HrCapacitacionempcal>
    {
        HrCapacitacionempcal obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion);
        HrCapacitacionempcal coreActualizar(UsuarioActual usuarioActual, HrCapacitacionempcal bean, String estado);
        HrCapacitacionempcal coreInsertar(UsuarioActual usuarioActual, HrCapacitacionempcal bean, String estado);
        void coreEliminar(HrCapacitacionempcalPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion);
        HrCapacitacionempcal coreAnular(UsuarioActual usuarioActual,HrCapacitacionempcalPk id);
        HrCapacitacionempcal coreAnular(UsuarioActual usuarioActual,String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion);
    }
}
