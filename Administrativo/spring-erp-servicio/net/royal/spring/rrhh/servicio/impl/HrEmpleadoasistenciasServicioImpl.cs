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

public class HrEmpleadoasistenciasServicioImpl : GenericoServicioImpl, HrEmpleadoasistenciasServicio {

        private IServiceProvider servicioProveedor;
        private HrEmpleadoasistenciasDao hrEmpleadoasistenciasDao;

        public HrEmpleadoasistenciasServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrEmpleadoasistenciasDao = servicioProveedor.GetService<HrEmpleadoasistenciasDao>();
        }

        public HrEmpleadoasistencias coreInsertar(UsuarioActual usuarioActual, HrEmpleadoasistencias bean)
        {
            return hrEmpleadoasistenciasDao.coreInsertar(usuarioActual,bean);
        }

        public HrEmpleadoasistencias coreActualizar(UsuarioActual usuarioActual, HrEmpleadoasistencias bean)
        {
            return hrEmpleadoasistenciasDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(HrEmpleadoasistenciasPk id)
        {
            hrEmpleadoasistenciasDao.coreEliminar(id);
        }

        public void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            hrEmpleadoasistenciasDao.coreEliminar( pCompanyowner, pCapacitacion, pEmpleado, pSecuencia);
        }


        public HrEmpleadoasistencias obtenerPorId(HrEmpleadoasistenciasPk id)
        {
            return hrEmpleadoasistenciasDao.obtenerPorId(id);
        }

        public HrEmpleadoasistencias obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            return hrEmpleadoasistenciasDao.obtenerPorId( pCompanyowner, pCapacitacion, pEmpleado, pSecuencia);
        }

    }
}
