using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio.impl
{

public class HrEmpleadoServicioImpl : GenericoServicioImpl, HrEmpleadoServicio {

        private IServiceProvider servicioProveedor;
        private HrEmpleadoDao hrEmpleadoDao;

        public HrEmpleadoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrEmpleadoDao = servicioProveedor.GetService<HrEmpleadoDao>();
}

}
}
