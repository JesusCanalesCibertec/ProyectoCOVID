using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.covid.dao;
using net.royal.spring.covid.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.covid.servicio.impl
{

public class PaisServicioImpl : GenericoServicioImpl, PaisServicio {

        private IServiceProvider servicioProveedor;
        private PaisDao paisDao;

        public PaisServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            paisDao = servicioProveedor.GetService<PaisDao>();
}

        public List<Pais> listarTodos()
        {
            return paisDao.listar();
        }

        public ParametroPaginacionGenerico listarUbigeoPorFiltro(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return paisDao.listarUbigeoPorFiltro(paginacion, filtro);
        }

        public DtoTabla obtenerUbigeo(String ubigeo)
        {
            return paisDao.obtenerUbigeo(ubigeo);
        }
    }
}
