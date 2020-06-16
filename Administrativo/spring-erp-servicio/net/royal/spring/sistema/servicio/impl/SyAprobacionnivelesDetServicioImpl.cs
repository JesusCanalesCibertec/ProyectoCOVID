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

public class SyAprobacionnivelesDetServicioImpl : GenericoServicioImpl, SyAprobacionnivelesDetServicio {

        private IServiceProvider servicioProveedor;
        private SyAprobacionnivelesDetDao syAprobacionnivelesDetDao;

        public SyAprobacionnivelesDetServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            syAprobacionnivelesDetDao = servicioProveedor.GetService<SyAprobacionnivelesDetDao>();
}

        public List<SyAprobacionnivelesDet> listarPorNivelAprobacion(SyAprobacionnivelesPk syAprobacionnivelesPk)
        {
            return syAprobacionnivelesDetDao.listarPorNivelAprobacion(syAprobacionnivelesPk);
        }

        public List<SyAprobacionnivelesDet> obtenerListaCorreoPorProceso(int? procesoAprobar, int? nivelAprobar)
        {
            return syAprobacionnivelesDetDao.obtenerListaCorreoPorProceso(procesoAprobar, nivelAprobar);
        }

        public List<SyAprobacionnivelesDet> listarPorCodigoNivel(Int32 codigo, Int32 nivel, String compania) {
            return syAprobacionnivelesDetDao.listarPorCodigoNivel(codigo, nivel, compania);
        }
    }
}
