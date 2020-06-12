using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrCapacitacionplanDao : GenericoDao<HrCapacitacionplan>
    {
        HrCapacitacionplan obtenerPorId(String pCompanyowner,Nullable<Decimal> pAnioplan);
        HrCapacitacionplan coreActualizar(UsuarioActual usuarioActual, HrCapacitacionplan bean, String estado);
        HrCapacitacionplan coreInsertar(UsuarioActual usuarioActual, HrCapacitacionplan bean, String estado);
        void coreEliminar(HrCapacitacionplanPk id);
        void coreEliminar(String pCompanyowner,Nullable<Decimal> pAnioplan);
        HrCapacitacionplan coreAnular(UsuarioActual usuarioActual,HrCapacitacionplanPk id);
        HrCapacitacionplan coreAnular(UsuarioActual usuarioActual,String pCompanyowner,Nullable<Decimal> pAnioplan);
    }
}
