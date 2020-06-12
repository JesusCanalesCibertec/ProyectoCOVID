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

    public class PmNotificacionpersonaServicioImpl : GenericoServicioImpl, PmNotificacionpersonaServicio
    {

        private IServiceProvider servicioProveedor;
        private PmNotificacionpersonaDao pmNotificacionpersonaDao;

        public PmNotificacionpersonaServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            pmNotificacionpersonaDao = servicioProveedor.GetService<PmNotificacionpersonaDao>();
        }
    }
}
