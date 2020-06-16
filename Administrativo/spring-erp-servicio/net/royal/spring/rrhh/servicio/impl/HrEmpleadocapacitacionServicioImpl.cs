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

public class HrEmpleadocapacitacionServicioImpl : GenericoServicioImpl, HrEmpleadocapacitacionServicio {

        private IServiceProvider servicioProveedor;
        private HrEmpleadocapacitacionDao hrEmpleadocapacitacionDao;

        public HrEmpleadocapacitacionServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrEmpleadocapacitacionDao = servicioProveedor.GetService<HrEmpleadocapacitacionDao>();
        }

        public HrEmpleadocapacitacion coreInsertar(UsuarioActual usuarioActual, HrEmpleadocapacitacion bean)
        {
            return hrEmpleadocapacitacionDao.coreInsertar(usuarioActual,bean);
        }

        public HrEmpleadocapacitacion coreActualizar(UsuarioActual usuarioActual, HrEmpleadocapacitacion bean)
        {
            return hrEmpleadocapacitacionDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(HrEmpleadocapacitacionPk id)
        {
            hrEmpleadocapacitacionDao.coreEliminar(id);
        }

        public void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado)
        {
            hrEmpleadocapacitacionDao.coreEliminar( pCompanyowner, pCapacitacion, pEmpleado);
        }


        public HrEmpleadocapacitacion obtenerPorId(HrEmpleadocapacitacionPk id)
        {
            return hrEmpleadocapacitacionDao.obtenerPorId(id);
        }

        public HrEmpleadocapacitacion obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado)
        {
            return hrEmpleadocapacitacionDao.obtenerPorId( pCompanyowner, pCapacitacion, pEmpleado);
        }

    }
}
