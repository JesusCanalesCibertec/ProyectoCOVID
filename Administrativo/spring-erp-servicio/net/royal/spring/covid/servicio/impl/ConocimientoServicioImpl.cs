using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.covid.dao;
using net.royal.spring.covid.dominio;

namespace net.royal.spring.covid.servicio.impl
{
    public class ConocimientoServicioImpl: GenericoServicioImpl, ConocimientoServicio
    {
        private IServiceProvider servicioProveedor;
        private ConocimientoDao conocimientoDao;

        public ConocimientoServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            conocimientoDao = servicioProveedor.GetService<ConocimientoDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return conocimientoDao.listarPaginacion(paginacion, filtro);
        }

        public Conocimiento registrar(UsuarioActual usuarioActual, Conocimiento bean)
        {
            bean.Nombre = UString.Mayusculas(bean.Nombre);
            bean.Descripcion = UString.Mayusculas(bean.Descripcion);
            return conocimientoDao.registrar(usuarioActual, bean);
        }

        public Conocimiento obtenerPorId(int pIdConocimiento)
        {
            return conocimientoDao.obtenerPorId(new Conocimiento() { IdConocimiento = pIdConocimiento}.obtenerArreglo());
        }

        public Conocimiento actualizar(UsuarioActual usuarioActual, Conocimiento bean)
        {
            bean.Nombre = UString.Mayusculas(bean.Nombre);
            bean.Descripcion = UString.Mayusculas(bean.Descripcion);
            return conocimientoDao.actualizar(usuarioActual, bean);
        }

        public ConocimientoPk cambiarestado(ConocimientoPk pk)
        {

            Conocimiento bean = conocimientoDao.obtenerPorId(pk.obtenerArreglo());

            if(bean.Estado == "A")
            {
                bean.Estado = "I";
            }
            else
            {
                bean.Estado = "A";
            }

            conocimientoDao.actualizar(bean);

            return pk;
        }

        public List<Conocimiento> listado(DtoTabla filtro)
        {
            return conocimientoDao.listado(filtro);
        }
    }
}
