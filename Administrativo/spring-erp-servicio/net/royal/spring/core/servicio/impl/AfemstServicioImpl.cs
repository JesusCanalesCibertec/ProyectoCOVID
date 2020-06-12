using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio.impl
{

    public class AfemstServicioImpl : GenericoServicioImpl, AfemstServicio
    {

        private IServiceProvider servicioProveedor;
        private AfemstDao afemstDao;

        public AfemstServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            afemstDao = servicioProveedor.GetService<AfemstDao>();
        }

        public List<Afemst> listar(FiltroAfe filtro)
        {
            return afemstDao.listar(filtro);
        }

        public List<Afemst> listarActivos()
        {
            FiltroAfe filtro = new FiltroAfe();
            filtro.Estado = "A";
            return listar(filtro);
        }

        public ParametroPaginacionGenerico listarPaginacion(DtoTabla filtro, ParametroPaginacionGenerico paginacion)
        {
            return afemstDao.listarPaginacion(filtro, paginacion);
        }

        public List<Afemst> listarTodos()
        {
            return afemstDao.listarTodos();
        }

        public Afemst obtenerporid(AfemstPk pk)
        {
            return afemstDao.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
