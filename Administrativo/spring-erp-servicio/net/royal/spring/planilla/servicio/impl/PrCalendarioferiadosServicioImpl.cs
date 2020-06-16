using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.planilla.dao;
using net.royal.spring.planilla.servicio;
using net.royal.spring.planilla.dominio;

namespace net.royal.spring.planilla.servicio.impl
{

    public class PrCalendarioferiadosServicioImpl : GenericoServicioImpl, PrCalendarioferiadosServicio
    {

        private IServiceProvider servicioProveedor;
        private PrCalendarioferiadosDao prCalendarioferiadosDao;

        public PrCalendarioferiadosServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            prCalendarioferiadosDao = servicioProveedor.GetService<PrCalendarioferiadosDao>();
        }

        public IList<PrCalendarioferiados> listarActivos()
        {
            return prCalendarioferiadosDao.listarActivos();
        }
    }
}
