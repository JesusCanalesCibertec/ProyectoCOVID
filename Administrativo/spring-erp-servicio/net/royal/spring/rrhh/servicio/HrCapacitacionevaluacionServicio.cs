using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrCapacitacionevaluacionServicio : GenericoServicio {
        HrCapacitacionevaluacion obtenerPorId(HrCapacitacionevaluacionPk id);
        HrCapacitacionevaluacion obtenerPorId(String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor);
        HrCapacitacionevaluacion coreActualizar(UsuarioActual usuarioActual, HrCapacitacionevaluacion bean);
        HrCapacitacionevaluacion coreInsertar(UsuarioActual usuarioActual, HrCapacitacionevaluacion bean);
        void coreEliminar(HrCapacitacionevaluacionPk id);
        void coreEliminar(String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor);
        HrCapacitacionevaluacion coreAnular(UsuarioActual usuarioActual,HrCapacitacionevaluacionPk id);
        HrCapacitacionevaluacion coreAnular(UsuarioActual usuarioActual,String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor);
    }
}
