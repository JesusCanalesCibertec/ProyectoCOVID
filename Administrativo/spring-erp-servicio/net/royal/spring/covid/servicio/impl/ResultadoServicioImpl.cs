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
    public class ResultadoServicioImpl : GenericoServicioImpl, ResultadoServicio
    {
        private IServiceProvider servicioProveedor;
        private ResultadoDao resultadoDao;
        private CiudadanoDao ciudadanoDao;

        public ResultadoServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            resultadoDao = servicioProveedor.GetService<ResultadoDao>();
            ciudadanoDao = servicioProveedor.GetService<CiudadanoDao>();
        }

        //public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroResultado filtro)
        //{
        //    return ResultadoDao.listarPaginacion(paginacion, filtro);
        //}

        public Resultado registrar(UsuarioActual usuarioActual, Resultado bean)
        {
           return resultadoDao.registrar(usuarioActual, bean);
        }

        public Resultado obtenerPorId(int pIdResultado)
        {
            return resultadoDao.obtenerPorId(new Resultado() { IdResultado = pIdResultado}.obtenerArreglo());
        }

        public Resultado actualizar(UsuarioActual usuarioActual, Resultado bean)
        {
            //bean.Nombre = UString.Mayusculas(bean.Nombre);
            //bean.Apellido = UString.Mayusculas(bean.Apellido);
            //bean.Direccion = UString.Mayusculas(bean.Direccion);
            return resultadoDao.actualizar(usuarioActual, bean);
        }

        public ResultadoPk cambiarestado(ResultadoPk pk)
        {
            return pk;
        }

        public List<Resultado> listado(DtoTabla filtro)
        {
            return resultadoDao.listado(filtro);
        }
    }
}
