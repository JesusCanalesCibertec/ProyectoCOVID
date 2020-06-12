using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.kpi.dao;
using net.royal.spring.kpi.servicio;
using net.royal.spring.kpi.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.salud.dominio;
using net.royal.spring.salud.dao;

namespace net.royal.spring.salud.servicio.impl {

    public class SsCie10capituloServicioImpl : GenericoServicioImpl, SsCie10capituloServicio {

        private IServiceProvider servicioProveedor;
        private SsCie10capituloDao ssCie10capituloDao;

        public SsCie10capituloServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            ssCie10capituloDao = servicioProveedor.GetService<SsCie10capituloDao>();
        }

        public List<SsCie10capitulo> listarTodos() {
            return ssCie10capituloDao.listarTodos();
        }

        public List<SsCie10capitulo> listarTodosActivos() {
            return ssCie10capituloDao.listarTodosActivos();
        }
    }
}
