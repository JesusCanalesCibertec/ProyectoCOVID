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
    public class CiudadanoServicioImpl: GenericoServicioImpl, CiudadanoServicio
    {
        private IServiceProvider servicioProveedor;
        private CiudadanoDao ciudadanoDao;

        public CiudadanoServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            ciudadanoDao = servicioProveedor.GetService<CiudadanoDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroCiudadano filtro)
        {
            return ciudadanoDao.listarPaginacion(paginacion, filtro);
        }

        public Ciudadano registrar(UsuarioActual usuarioActual, Ciudadano bean)
        {
            Ciudadano existePersona = ciudadanoDao.ObtenerPersonaxDNI(bean.NroDocumento);
            if (existePersona != null)
            {
                return null;
            }
            bean.Nombre = UString.Mayusculas(bean.Nombre);
            bean.Apellido = UString.Mayusculas(bean.Apellido);
            bean.Direccion = UString.Mayusculas(bean.Direccion);
            return ciudadanoDao.registrar(usuarioActual, bean);
        }

        public Ciudadano obtenerPorId(int pIdCiudadano)
        {
            return ciudadanoDao.obtenerPorId(new Ciudadano() { IdCiudadano = pIdCiudadano}.obtenerArreglo());
        }

        public Ciudadano actualizar(UsuarioActual usuarioActual, Ciudadano bean)
        {
            bean.Nombre = UString.Mayusculas(bean.Nombre);
            bean.Apellido = UString.Mayusculas(bean.Apellido);
            bean.Direccion = UString.Mayusculas(bean.Direccion);
            return ciudadanoDao.actualizar(usuarioActual, bean);
        }

        public CiudadanoPk cambiarestado(CiudadanoPk pk)
        {
            return pk;
        }

        public List<Ciudadano> listado(DtoTabla filtro)
        {
            return ciudadanoDao.listado(filtro);
        }

        public List<Ciudadano> listado()
        {
            return ciudadanoDao.listarTodos();
        }

        public List<DtoTabla> ListarPie()
        {
            return ciudadanoDao.ListarPie();
        }

        public List<DtoTabla> listarPiexDepartamento(string pDepa)
        {
            return ciudadanoDao.listarPiexDepartamento(pDepa);
        }

        public List<DtoTabla> listarPiexProvincia(string pDepa, string pProv)
        {
            return ciudadanoDao.listarPiexProvincia(pDepa,pProv);
        }

        public List<DtoTabla> listarPiexDistrito(string pDepa, string pProv, string pDist)
        {
            return ciudadanoDao.listarPiexDistrito(pDepa,pProv,pDist);
        }
    }
}
