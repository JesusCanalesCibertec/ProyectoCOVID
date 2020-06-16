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

public class HrCapacitacionempcalServicioImpl : GenericoServicioImpl, HrCapacitacionempcalServicio {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionempcalDao hrCapacitacionempcalDao;

        public HrCapacitacionempcalServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionempcalDao = servicioProveedor.GetService<HrCapacitacionempcalDao>();
        }

        public HrCapacitacionempcal coreInsertar(UsuarioActual usuarioActual, HrCapacitacionempcal bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return hrCapacitacionempcalDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public HrCapacitacionempcal coreActualizar(UsuarioActual usuarioActual, HrCapacitacionempcal bean)
        {
            return hrCapacitacionempcalDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public HrCapacitacionempcal coreAnular(UsuarioActual usuarioActual, HrCapacitacionempcalPk id)
        {
            return hrCapacitacionempcalDao.coreAnular(usuarioActual,id);
        }

        public HrCapacitacionempcal coreAnular(UsuarioActual usuarioActual, String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion)
        {
            return hrCapacitacionempcalDao.coreAnular(usuarioActual,  pCompanyowner, pCapacitacion, pEmpleado, pEvaluacion);
        }

        public void coreEliminar(HrCapacitacionempcalPk id)
        {
            hrCapacitacionempcalDao.coreEliminar(id);
        }

        public void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion)
        {
            hrCapacitacionempcalDao.coreEliminar( pCompanyowner, pCapacitacion, pEmpleado, pEvaluacion);
        }


        public HrCapacitacionempcal obtenerPorId(HrCapacitacionempcalPk id)
        {
            return hrCapacitacionempcalDao.obtenerPorId(id);
        }

        public HrCapacitacionempcal obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion)
        {
            return hrCapacitacionempcalDao.obtenerPorId( pCompanyowner, pCapacitacion, pEmpleado, pEvaluacion);
        }

    }
}
