using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core.servicio.impl
{

    public class CompaniamastServicioImpl : GenericoServicioImpl, CompaniamastServicio
    {

        private IServiceProvider servicioProveedor;
        private CompaniamastDao companiamastDao;

        public CompaniamastServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            companiamastDao = servicioProveedor.GetService<CompaniamastDao>();
        }

        public List<Companiamast> listarTodos()
        {
            return companiamastDao.listarTodos();
        }

        public Companiamast obtenerPorId(CompaniamastPk pk)
        {
            return companiamastDao.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
