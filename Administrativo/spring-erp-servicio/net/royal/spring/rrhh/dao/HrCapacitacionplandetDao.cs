using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrCapacitacionplandetDao : GenericoDao<HrCapacitacionplandet>
    {
        HrCapacitacionplandet obtenerPorId(String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion);
        HrCapacitacionplandet coreActualizar(UsuarioActual usuarioActual, HrCapacitacionplandet bean, String estado);
        HrCapacitacionplandet coreInsertar(UsuarioActual usuarioActual, HrCapacitacionplandet bean, String estado);
        void coreEliminar(HrCapacitacionplandetPk id);
        void coreEliminar(String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion);
        HrCapacitacionplandet coreAnular(UsuarioActual usuarioActual,HrCapacitacionplandetPk id);
        HrCapacitacionplandet coreAnular(UsuarioActual usuarioActual,String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion);
    }
}
