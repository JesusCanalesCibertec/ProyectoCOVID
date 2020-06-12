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

    public class PrTipoplanillaServicioImpl : GenericoServicioImpl, PrTipoplanillaServicio
    {

        private IServiceProvider servicioProveedor;
        private PrTipoplanillaDao prTipoplanillaDao;

        public PrTipoplanillaServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            prTipoplanillaDao = servicioProveedor.GetService<PrTipoplanillaDao>();
        }

        public List<PrTipoplanilla> listarTodos()
        {
            return prTipoplanillaDao.listarTodos();
        }

        public PrTipoplanilla obtenerPorId(PrTipoplanillaPk obj)
        {
            return prTipoplanillaDao.obtenerPorId(obj.obtenerArreglo());
        }
    }
}
