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

    public class SeguridadconceptoServicioImpl : GenericoServicioImpl, SeguridadconceptoServicio {

        private IServiceProvider servicioProveedor;
        private SeguridadconceptoDao seguridadconceptoDao;

        public SeguridadconceptoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            seguridadconceptoDao = servicioProveedor.GetService<SeguridadconceptoDao>();
        }

        public Seguridadconcepto obtenerPorId(SeguridadconceptoPk pk)
        {
            return seguridadconceptoDao.obtenerPorId(pk);
        }
    }
}