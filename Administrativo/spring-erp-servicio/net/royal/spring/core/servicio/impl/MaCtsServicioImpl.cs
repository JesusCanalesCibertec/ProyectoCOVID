using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio.impl
{

    public class MaCtsServicioImpl : GenericoServicioImpl, MaCtsServicio {

        private IServiceProvider servicioProveedor;
        private MaCtsDao maCtsDao;

        public MaCtsServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            maCtsDao = servicioProveedor.GetService<MaCtsDao>();
        }

        public List<MaCts> listar(DtoFiltroEntero filtro)
        {
            return maCtsDao.listar(filtro);
        }

        public List<MaCts> listarActivos()
        {
            DtoFiltroEntero filtro = new DtoFiltroEntero();
            filtro.Estado = "A";
            return listar(filtro);
        }

        public List<MaCts> listarTodos()
        {
            return maCtsDao.listarTodos();
        }
    }
}
