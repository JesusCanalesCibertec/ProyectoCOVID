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

public class HrEmpleadocaphorarioServicioImpl : GenericoServicioImpl, HrEmpleadocaphorarioServicio {

        private IServiceProvider servicioProveedor;
        private HrEmpleadocaphorarioDao hrEmpleadocaphorarioDao;

        public HrEmpleadocaphorarioServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrEmpleadocaphorarioDao = servicioProveedor.GetService<HrEmpleadocaphorarioDao>();
        }

        public HrEmpleadocaphorario coreInsertar(UsuarioActual usuarioActual, HrEmpleadocaphorario bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return hrEmpleadocaphorarioDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public HrEmpleadocaphorario coreActualizar(UsuarioActual usuarioActual, HrEmpleadocaphorario bean)
        {
            return hrEmpleadocaphorarioDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public HrEmpleadocaphorario coreAnular(UsuarioActual usuarioActual, HrEmpleadocaphorarioPk id)
        {
            return hrEmpleadocaphorarioDao.coreAnular(usuarioActual,id);
        }

        public HrEmpleadocaphorario coreAnular(UsuarioActual usuarioActual, String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            return hrEmpleadocaphorarioDao.coreAnular(usuarioActual,  pCompanyowner, pCapacitacion, pEmpleado, pSecuencia);
        }

        public void coreEliminar(HrEmpleadocaphorarioPk id)
        {
            hrEmpleadocaphorarioDao.coreEliminar(id);
        }

        public void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            hrEmpleadocaphorarioDao.coreEliminar( pCompanyowner, pCapacitacion, pEmpleado, pSecuencia);
        }


        public HrEmpleadocaphorario obtenerPorId(HrEmpleadocaphorarioPk id)
        {
            return hrEmpleadocaphorarioDao.obtenerPorId(id);
        }

        public HrEmpleadocaphorario obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            return hrEmpleadocaphorarioDao.obtenerPorId( pCompanyowner, pCapacitacion, pEmpleado, pSecuencia);
        }

    }
}
