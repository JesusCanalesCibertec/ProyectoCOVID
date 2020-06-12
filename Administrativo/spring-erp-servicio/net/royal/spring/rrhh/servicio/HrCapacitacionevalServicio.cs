using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrCapacitacionevalServicio : GenericoServicio {
        HrCapacitacioneval obtenerPorId(HrCapacitacionevalPk id);
        HrCapacitacioneval obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion);
        HrCapacitacioneval coreActualizar(UsuarioActual usuarioActual, HrCapacitacioneval bean);
        HrCapacitacioneval coreInsertar(UsuarioActual usuarioActual, HrCapacitacioneval bean);
        void coreEliminar(HrCapacitacionevalPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion);
        HrCapacitacioneval coreAnular(UsuarioActual usuarioActual,HrCapacitacionevalPk id);
        HrCapacitacioneval coreAnular(UsuarioActual usuarioActual,String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion);
    }
}
