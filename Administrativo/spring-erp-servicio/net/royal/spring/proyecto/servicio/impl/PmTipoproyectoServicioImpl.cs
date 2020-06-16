using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.proyecto.servicio.impl
{

    public class PmTipoproyectoServicioImpl : GenericoServicioImpl, PmTipoproyectoServicio
    {

        private IServiceProvider servicioProveedor;
        private PmTipoproyectoDao pmTipoproyectoDao;

        public PmTipoproyectoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            pmTipoproyectoDao = servicioProveedor.GetService<PmTipoproyectoDao>();
        }

        public PmTipoproyecto obtenerPorId(String id)
        {
            return pmTipoproyectoDao.obtenerPorId(id);
        }
    }
}
