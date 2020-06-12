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

namespace net.royal.spring.salud.servicio.impl
{

public class SsCie10grupoServicioImpl : GenericoServicioImpl, SsCie10grupoServicio
    {

        private IServiceProvider servicioProveedor;
        private SsCie10grupoDao ssCie10grupoDao;

        public SsCie10grupoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            ssCie10grupoDao = servicioProveedor.GetService<SsCie10grupoDao>();
        }

        public List<SsCie10grupo> listarPorCapitulo(string codigo)
        {
            return ssCie10grupoDao.listarPorCapitulo(codigo);
        }
    }
}
