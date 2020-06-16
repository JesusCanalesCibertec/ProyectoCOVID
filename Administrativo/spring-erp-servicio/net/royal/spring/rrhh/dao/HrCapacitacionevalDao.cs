using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrCapacitacionevalDao : GenericoDao<HrCapacitacioneval>
    {
        HrCapacitacioneval obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion);
        HrCapacitacioneval coreActualizar(UsuarioActual usuarioActual, HrCapacitacioneval bean, String estado);
        HrCapacitacioneval coreInsertar(UsuarioActual usuarioActual, HrCapacitacioneval bean, String estado);
        void coreEliminar(HrCapacitacionevalPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion);
        HrCapacitacioneval coreAnular(UsuarioActual usuarioActual,HrCapacitacionevalPk id);
        HrCapacitacioneval coreAnular(UsuarioActual usuarioActual,String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion);
    }
}
