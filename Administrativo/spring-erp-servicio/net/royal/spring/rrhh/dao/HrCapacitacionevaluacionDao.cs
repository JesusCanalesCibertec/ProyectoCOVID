using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrCapacitacionevaluacionDao : GenericoDao<HrCapacitacionevaluacion>
    {
        HrCapacitacionevaluacion obtenerPorId(String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor);
        HrCapacitacionevaluacion coreActualizar(UsuarioActual usuarioActual, HrCapacitacionevaluacion bean, String estado);
        HrCapacitacionevaluacion coreInsertar(UsuarioActual usuarioActual, HrCapacitacionevaluacion bean, String estado);
        void coreEliminar(HrCapacitacionevaluacionPk id);
        void coreEliminar(String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor);
        HrCapacitacionevaluacion coreAnular(UsuarioActual usuarioActual,HrCapacitacionevaluacionPk id);
        HrCapacitacionevaluacion coreAnular(UsuarioActual usuarioActual,String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor);
    }
}
