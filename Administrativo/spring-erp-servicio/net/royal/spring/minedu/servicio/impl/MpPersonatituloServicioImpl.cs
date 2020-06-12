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
using net.royal.spring.minedu.dominio.dto;

namespace net.royal.spring.minedu.servicio.impl
{
    public class MpPersonatituloServicioImpl: GenericoServicioImpl, MpPersonatituloServicio
    {
        private IServiceProvider servicioProveedor;
        private MpPersonatituloDao MpPersonatituloDao;

        public MpPersonatituloServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            MpPersonatituloDao = servicioProveedor.GetService<MpPersonatituloDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return MpPersonatituloDao.listarPaginacion(paginacion, filtro);
        }

        public MpPersonatitulo registrar(UsuarioActual usuarioActual, MpPersonatitulo bean)
        {
            return MpPersonatituloDao.registrar(usuarioActual, bean);
        }

        public MpPersonatitulo obtenerPorId(int pIdPersona)
        {
            return MpPersonatituloDao.obtenerPorId(new MpPersonatitulo() { IdPersona = pIdPersona}.obtenerArreglo());
        }

        public MpPersonatitulo actualizar(UsuarioActual usuarioActual, MpPersonatitulo bean)
        {
            return MpPersonatituloDao.actualizar(usuarioActual, bean);
        }

        public MpPersonatituloPk cambiarestado(MpPersonatituloPk pk)
        {

            MpPersonatitulo bean = MpPersonatituloDao.obtenerPorId(pk.obtenerArreglo());

            if(bean.Estado == "A")
            {
                bean.Estado = "I";
            }
            else
            {
                bean.Estado = "A";
            }

            MpPersonatituloDao.actualizar(bean);

            return pk;
        }

        public List<DtoPersonatitulo> listado(int? pIdPersona)
        {
            return MpPersonatituloDao.listado(pIdPersona);
        }

        public void eliminar(MpPersonatitulo bean)
        {
            MpPersonatituloDao.eliminar(bean);     
        }

        public MpPersonatitulo obtenerPorId(MpPersonatituloPk pk)
        {
            return MpPersonatituloDao.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
