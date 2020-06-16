using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.framework.core;
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.constante;
using Microsoft.Extensions.Logging;

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpColorServicioImpl : GenericoServicioImpl, BpColorServicio
    {

        private IServiceProvider servicioProveedor;
        private BpColorDao BpColorDao;


        public BpColorServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            BpColorDao = servicioProveedor.GetService<BpColorDao>();
        }

        public List<BpColor> listarTodos()
        {
            return BpColorDao.listarTodos();
        }
    }
}
