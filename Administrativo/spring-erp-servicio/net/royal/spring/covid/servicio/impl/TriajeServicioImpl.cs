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
using net.royal.spring.covid.dominio.filtro;

namespace net.royal.spring.covid.servicio.impl
{
    public class TriajeServicioImpl: GenericoServicioImpl, TriajeServicio
    {
        private IServiceProvider servicioProveedor;
        private TriajeDao triajeDao;

        public TriajeServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            triajeDao = servicioProveedor.GetService<TriajeDao>();
        }

        //public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroTriaje filtro)
        //{
        //    return triajeDao.listarPaginacion(paginacion, filtro);
        //}

        public Triaje registrar(UsuarioActual usuarioActual, Triaje bean)
        {
            //bean.Nombre = UString.Mayusculas(bean.Nombre);
            //bean.Apellido = UString.Mayusculas(bean.Apellido);
            //bean.Direccion = UString.Mayusculas(bean.Direccion);
            return triajeDao.registrar(usuarioActual, bean);
        }

        public Triaje obtenerPorId(int pIdTriaje)
        {
            return triajeDao.obtenerPorId(new Triaje() { IdTriaje = pIdTriaje}.obtenerArreglo());
        }

        public Triaje actualizar(UsuarioActual usuarioActual, Triaje bean)
        {
            //bean.Nombre = UString.Mayusculas(bean.Nombre);
            //bean.Apellido = UString.Mayusculas(bean.Apellido);
            //bean.Direccion = UString.Mayusculas(bean.Direccion);
            return triajeDao.actualizar(usuarioActual, bean);
        }

        public TriajePk cambiarestado(TriajePk pk)
        {
            return pk;
        }

        public List<Triaje> listado(DtoTabla filtro)
        {
            return triajeDao.listado(filtro);
        }
    }
}
