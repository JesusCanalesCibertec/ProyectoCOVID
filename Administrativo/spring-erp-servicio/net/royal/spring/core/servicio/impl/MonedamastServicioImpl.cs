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

public class MonedamastServicioImpl : GenericoServicioImpl, MonedamastServicio {

        private IServiceProvider servicioProveedor;
        private MonedamastDao monedamastDao;

        public MonedamastServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            monedamastDao = servicioProveedor.GetService<MonedamastDao>();
        }

        public List<Monedamast> listarActivos()
        {
            return monedamastDao.listarActivos();
        }

        public List<Monedamast> listarTodos()
        {
            return monedamastDao.listarTodos();
        }
    }
}
