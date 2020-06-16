using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.minedu.dao;
using net.royal.spring.minedu.dominio;

namespace net.royal.spring.minedu.servicio.impl
{
    public class MpConocimientoServicioImpl: GenericoServicioImpl, MpConocimientoServicio
    {
        private IServiceProvider servicioProveedor;
        private MpConocimientoDao mpConocimientoDao;

        public MpConocimientoServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            mpConocimientoDao = servicioProveedor.GetService<MpConocimientoDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return mpConocimientoDao.listarPaginacion(paginacion, filtro);
        }

        public MpConocimiento registrar(UsuarioActual usuarioActual, MpConocimiento bean)
        {
            bean.Nombre = UString.Mayusculas(bean.Nombre);
            bean.Descripcion = UString.Mayusculas(bean.Descripcion);
            return mpConocimientoDao.registrar(usuarioActual, bean);
        }

        public MpConocimiento obtenerPorId(int pIdConocimiento)
        {
            return mpConocimientoDao.obtenerPorId(new MpConocimiento() { IdConocimiento = pIdConocimiento}.obtenerArreglo());
        }

        public MpConocimiento actualizar(UsuarioActual usuarioActual, MpConocimiento bean)
        {
            bean.Nombre = UString.Mayusculas(bean.Nombre);
            bean.Descripcion = UString.Mayusculas(bean.Descripcion);
            return mpConocimientoDao.actualizar(usuarioActual, bean);
        }

        public MpConocimientoPk cambiarestado(MpConocimientoPk pk)
        {

            MpConocimiento bean = mpConocimientoDao.obtenerPorId(pk.obtenerArreglo());

            if(bean.Estado == "A")
            {
                bean.Estado = "I";
            }
            else
            {
                bean.Estado = "A";
            }

            mpConocimientoDao.actualizar(bean);

            return pk;
        }

        public List<MpConocimiento> listado(DtoTabla filtro)
        {
            return mpConocimientoDao.listado(filtro);
        }
    }
}
