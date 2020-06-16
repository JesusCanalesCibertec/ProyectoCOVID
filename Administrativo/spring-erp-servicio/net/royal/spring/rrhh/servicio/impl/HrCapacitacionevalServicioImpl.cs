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

public class HrCapacitacionevalServicioImpl : GenericoServicioImpl, HrCapacitacionevalServicio {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionevalDao hrCapacitacionevalDao;

        public HrCapacitacionevalServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionevalDao = servicioProveedor.GetService<HrCapacitacionevalDao>();
        }

        public HrCapacitacioneval coreInsertar(UsuarioActual usuarioActual, HrCapacitacioneval bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return hrCapacitacionevalDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public HrCapacitacioneval coreActualizar(UsuarioActual usuarioActual, HrCapacitacioneval bean)
        {
            return hrCapacitacionevalDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public HrCapacitacioneval coreAnular(UsuarioActual usuarioActual, HrCapacitacionevalPk id)
        {
            return hrCapacitacionevalDao.coreAnular(usuarioActual,id);
        }

        public HrCapacitacioneval coreAnular(UsuarioActual usuarioActual, String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion)
        {
            return hrCapacitacionevalDao.coreAnular(usuarioActual,  pCompanyowner, pCapacitacion, pEvaluacion);
        }

        public void coreEliminar(HrCapacitacionevalPk id)
        {
            hrCapacitacionevalDao.coreEliminar(id);
        }

        public void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion)
        {
            hrCapacitacionevalDao.coreEliminar( pCompanyowner, pCapacitacion, pEvaluacion);
        }


        public HrCapacitacioneval obtenerPorId(HrCapacitacionevalPk id)
        {
            return hrCapacitacionevalDao.obtenerPorId(id);
        }

        public HrCapacitacioneval obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion)
        {
            return hrCapacitacionevalDao.obtenerPorId( pCompanyowner, pCapacitacion, pEvaluacion);
        }

    }
}
