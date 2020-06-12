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

public class SeguridadgrupoServicioImpl : GenericoServicioImpl, SeguridadgrupoServicio {

        private IServiceProvider servicioProveedor;
        private SeguridadgrupoDao seguridadgrupoDao;

        public SeguridadgrupoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            seguridadgrupoDao = servicioProveedor.GetService<SeguridadgrupoDao>();
}

}
}
