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

public class HrCapacitacionEmpleadoServicioImpl : GenericoServicioImpl, HrCapacitacionEmpleadoServicio {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionEmpleadoDao hrCapacitacionEmpleadoDao;

        public HrCapacitacionEmpleadoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionEmpleadoDao = servicioProveedor.GetService<HrCapacitacionEmpleadoDao>();
        }

        public HrCapacitacionEmpleado coreInsertar(UsuarioActual usuarioActual, HrCapacitacionEmpleado bean)
        {
            return hrCapacitacionEmpleadoDao.coreInsertar(usuarioActual,bean);
        }

        public HrCapacitacionEmpleado coreActualizar(UsuarioActual usuarioActual, HrCapacitacionEmpleado bean)
        {
            return hrCapacitacionEmpleadoDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(HrCapacitacionEmpleadoPk id)
        {
            hrCapacitacionEmpleadoDao.coreEliminar(id);
        }

        public void coreEliminar(String pCompanyowner,String pCapacitacion,String pIdInstitucion,Nullable<Int32> pIdEmpleado)
        {
            hrCapacitacionEmpleadoDao.coreEliminar( pCompanyowner, pCapacitacion, pIdInstitucion, pIdEmpleado);
        }


        public HrCapacitacionEmpleado obtenerPorId(HrCapacitacionEmpleadoPk id)
        {
            return hrCapacitacionEmpleadoDao.obtenerPorId(id);
        }

        public HrCapacitacionEmpleado obtenerPorId(String pCompanyowner,String pCapacitacion,String pIdInstitucion,Nullable<Int32> pIdEmpleado)
        {
            return hrCapacitacionEmpleadoDao.obtenerPorId( pCompanyowner, pCapacitacion, pIdInstitucion, pIdEmpleado);
        }

    }
}
