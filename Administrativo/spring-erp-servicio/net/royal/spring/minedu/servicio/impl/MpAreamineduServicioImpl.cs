using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.minedu.dao;
using net.royal.spring.minedu.dominio;

namespace net.royal.spring.minedu.servicio.impl
{
    public class MpAreamineduServicioImpl: GenericoServicioImpl, MpAreamineduServicio
    {
        private IServiceProvider servicioProveedor;
        private MpAreamineduDao MpAreamineduDao;

        public MpAreamineduServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            MpAreamineduDao = servicioProveedor.GetService<MpAreamineduDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return MpAreamineduDao.listarPaginacion(paginacion, filtro);
        }

  
    }
}
