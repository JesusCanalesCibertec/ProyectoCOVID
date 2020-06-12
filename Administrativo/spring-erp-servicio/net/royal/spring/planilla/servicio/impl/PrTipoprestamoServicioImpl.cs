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

    public class PrTipoprestamoServicioImpl : GenericoServicioImpl, PrTipoprestamoServicio
    {

        private IServiceProvider servicioProveedor;
        private PrTipoprestamoDao prTipoprestamoDao;

        public PrTipoprestamoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            prTipoprestamoDao = servicioProveedor.GetService<PrTipoprestamoDao>();
        }

        public List<PrTipoprestamo> listarActivos()
        {
            return prTipoprestamoDao.listarActivos();
        }

        public List<PrTipoprestamo> listarSoloWeb()
        {
            return prTipoprestamoDao.listarSoloWeb();
        }

        public List<PrTipoprestamo> listarTodos()
        {
            return prTipoprestamoDao.listarTodos();
        }

        public PrTipoprestamo obtenerPorId(PrTipoprestamoPk pk) {
            return prTipoprestamoDao.obtenerPorId(pk);
        }
    }
}
