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

public class SsCie10subgrupoServicioImpl : GenericoServicioImpl, SsCie10subgrupoServicio
    {

        private IServiceProvider servicioProveedor;
        private SsCie10subgrupoDao ssCie10subgrupoDao;

        public SsCie10subgrupoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            ssCie10subgrupoDao = servicioProveedor.GetService<SsCie10subgrupoDao>();
        }

        public List<SsCie10subgrupo> listarPorGrupo(string codigo)
        {
            return ssCie10subgrupoDao.listarPorGrupo(codigo);
        }
    }
}
