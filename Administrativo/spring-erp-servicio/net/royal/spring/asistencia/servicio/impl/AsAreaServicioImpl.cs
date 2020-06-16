using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.asistencia.servicio.impl
{

    public class AsAreaServicioImpl : GenericoServicioImpl, AsAreaServicio
    {

        private IServiceProvider servicioProveedor;
        private AsAreaDao asAreaDao;

        public AsAreaServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            asAreaDao = servicioProveedor.GetService<AsAreaDao>();
        }

        public List<AsArea> listarTodos()
        {
            return asAreaDao.listarTodos();
        }
    }
}
