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

public class AprobacionRrhhServicioImpl : GenericoServicioImpl, AprobacionRrhhServicio {

        private IServiceProvider servicioProveedor;
        private AprobacionRrhhDao aprobacionRrhhDao;

        public AprobacionRrhhServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            aprobacionRrhhDao = servicioProveedor.GetService<AprobacionRrhhDao>();
        }

        public List<AprobacionRrhh> listarTodos()
        {
            return aprobacionRrhhDao.listarTodos();
        }
    }
}
