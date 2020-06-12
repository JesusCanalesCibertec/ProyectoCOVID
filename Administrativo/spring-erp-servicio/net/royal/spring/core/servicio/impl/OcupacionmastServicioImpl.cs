using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core.servicio.impl
{

public class OcupacionmastServicioImpl : GenericoServicioImpl, OcupacionmastServicio {

        private IServiceProvider servicioProveedor;
        private OcupacionmastDao ocupacionmastDao;

        public OcupacionmastServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            ocupacionmastDao = servicioProveedor.GetService<OcupacionmastDao>();
}

        public List<Ocupacionmast> listarTodos()
        {
            return ocupacionmastDao.listarTodos();
        }
    }
}
