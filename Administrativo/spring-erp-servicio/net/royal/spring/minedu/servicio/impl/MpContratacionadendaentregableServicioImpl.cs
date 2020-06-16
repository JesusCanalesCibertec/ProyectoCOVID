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
    public class MpContratacionadendaentregableServicioImpl: GenericoServicioImpl, MpContratacionadendaentregableServicio
    {
        private IServiceProvider servicioProveedor;
        private MpContratacionadendaentregableDao mpContratacionadendaentregableDao;

        public MpContratacionadendaentregableServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            mpContratacionadendaentregableDao = servicioProveedor.GetService<MpContratacionadendaentregableDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return mpContratacionadendaentregableDao.listarPaginacion(paginacion, filtro);
        }

        public MpContratacionadendaentregable registrar(UsuarioActual usuarioActual, MpContratacionadendaentregable bean)
        {
            bean.Descripcion = UString.Mayusculas(bean.Descripcion);
            return mpContratacionadendaentregableDao.registrar(usuarioActual, bean);
        }

        public MpContratacionadendaentregable obtenerPorId(int pIdContratacionadendaentregable)
        {
            return mpContratacionadendaentregableDao.obtenerPorId(new MpContratacionadendaentregable() { IdCodigo = pIdContratacionadendaentregable}.obtenerArreglo());
        }

        public MpContratacionadendaentregable actualizar(UsuarioActual usuarioActual, MpContratacionadendaentregable bean)
        {
            bean.Descripcion = UString.Mayusculas(bean.Descripcion);
            return mpContratacionadendaentregableDao.actualizar(usuarioActual, bean);
        }

        public MpContratacionadendaentregablePk cambiarestado(MpContratacionadendaentregablePk pk)
        {

            MpContratacionadendaentregable bean = mpContratacionadendaentregableDao.obtenerPorId(pk.obtenerArreglo());

            if(bean.Estado == "A")
            {
                bean.Estado = "I";
            }
            else
            {
                bean.Estado = "A";
            }

            mpContratacionadendaentregableDao.actualizar(bean);

            return pk;
        }

        public List<MpContratacionadendaentregable> listado(DtoTabla filtro)
        {
            return mpContratacionadendaentregableDao.listado(filtro);
        }

        public List<MpContratacionadendaentregable> listado(int? pIdContratacion)
        {
            return mpContratacionadendaentregableDao.listado(pIdContratacion);
        }

        
    }
}
