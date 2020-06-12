using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.core.servicio.impl
{

    public class PmPublicacionServicioImpl : GenericoServicioImpl, PmPublicacionServicio
    {

        private IServiceProvider servicioProveedor;
        private PmPublicacionDao pmPublicacionDao;

        public PmPublicacionServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            pmPublicacionDao = servicioProveedor.GetService<PmPublicacionDao>();
        }

        public ParametroPaginacionGenerico listarBusqueda(FiltroPaginacionPublicaciones filtro)
        {
            return pmPublicacionDao.listarBusqueda(filtro);
        }

        public IList<DtoTabla> listarPublicacionesDashboard()
        {
            return pmPublicacionDao.listarPublicacionesDashboard();
        }

        public PmPublicacion obtenerPorid(PmPublicacionPk pk)
        {
            var publicacion = pmPublicacionDao.obtenerPorId(pk.obtenerArreglo());
            if (publicacion != null)
            {
                publicacion.PublicacionString = UByte.convertirString(publicacion.Publicacion);
            }
            return publicacion;
        }

        public PmPublicacion registrar(PmPublicacion bean, UsuarioActual usuarioActual)
        {
            bean.Publicacion = UByte.convertirByte(bean.PublicacionString);
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            bean.IdPubicacion = pmPublicacionDao.generarId(bean.IdAplicacion);
            return pmPublicacionDao.registrar(bean);
        }

        public PmPublicacion actualizar(PmPublicacion bean, UsuarioActual usuarioActual)
        {
            bean.Publicacion = UByte.convertirByte(bean.PublicacionString);
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            pmPublicacionDao.actualizar(bean);
            return bean;
        }

        public IList<DtoTabla> listarIndicadores(UsuarioActual usuarioActual)
        {
            return pmPublicacionDao.listarIndicadores(usuarioActual);
        }
    }
}
