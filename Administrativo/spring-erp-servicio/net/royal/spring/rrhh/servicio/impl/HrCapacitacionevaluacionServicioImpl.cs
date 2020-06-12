using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.rrhh.servicio.impl
{

public class HrCapacitacionevaluacionServicioImpl : GenericoServicioImpl, HrCapacitacionevaluacionServicio {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionevaluacionDao hrCapacitacionevaluacionDao;

        public HrCapacitacionevaluacionServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionevaluacionDao = servicioProveedor.GetService<HrCapacitacionevaluacionDao>();
        }

        public HrCapacitacionevaluacion coreInsertar(UsuarioActual usuarioActual, HrCapacitacionevaluacion bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return hrCapacitacionevaluacionDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public HrCapacitacionevaluacion coreActualizar(UsuarioActual usuarioActual, HrCapacitacionevaluacion bean)
        {
            return hrCapacitacionevaluacionDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public HrCapacitacionevaluacion coreAnular(UsuarioActual usuarioActual, HrCapacitacionevaluacionPk id)
        {
            return hrCapacitacionevaluacionDao.coreAnular(usuarioActual,id);
        }

        public HrCapacitacionevaluacion coreAnular(UsuarioActual usuarioActual, String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor)
        {
            return hrCapacitacionevaluacionDao.coreAnular(usuarioActual,  pCapacitacion, pSecuenciaempleado, pFactor);
        }

        public void coreEliminar(HrCapacitacionevaluacionPk id)
        {
            hrCapacitacionevaluacionDao.coreEliminar(id);
        }

        public void coreEliminar(String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor)
        {
            hrCapacitacionevaluacionDao.coreEliminar( pCapacitacion, pSecuenciaempleado, pFactor);
        }


        public HrCapacitacionevaluacion obtenerPorId(HrCapacitacionevaluacionPk id)
        {
            return hrCapacitacionevaluacionDao.obtenerPorId(id);
        }

        public HrCapacitacionevaluacion obtenerPorId(String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor)
        {
            return hrCapacitacionevaluacionDao.obtenerPorId( pCapacitacion, pSecuenciaempleado, pFactor);
        }

    }
}
