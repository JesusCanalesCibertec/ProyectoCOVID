using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema.servicio.impl
{

public class SeguridadperfilusuarioServicioImpl : GenericoServicioImpl, SeguridadperfilusuarioServicio {

        private IServiceProvider servicioProveedor;
        private SeguridadperfilusuarioDao seguridadperfilusuarioDao;

        public SeguridadperfilusuarioServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            seguridadperfilusuarioDao = servicioProveedor.GetService<SeguridadperfilusuarioDao>();
}

}
}
