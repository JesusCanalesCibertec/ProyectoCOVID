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
    public class MpPersonaconocimientoServicioImpl: GenericoServicioImpl, MpPersonaconocimientoServicio
    {
        private IServiceProvider servicioProveedor;
        private MpPersonaconocimientoDao MpPersonaconocimientoDao;

        public MpPersonaconocimientoServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            MpPersonaconocimientoDao = servicioProveedor.GetService<MpPersonaconocimientoDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return MpPersonaconocimientoDao.listarPaginacion(paginacion, filtro);
        }

        public MpPersonaconocimiento registrar(UsuarioActual usuarioActual, MpPersonaconocimiento bean)
        {
            return MpPersonaconocimientoDao.registrar(usuarioActual, bean);
        }

        public MpPersonaconocimiento obtenerPorId(int pIdPersona)
        {
            return MpPersonaconocimientoDao.obtenerPorId(new MpPersonaconocimiento() { IdPersona = pIdPersona}.obtenerArreglo());
        }

        public MpPersonaconocimiento actualizar(UsuarioActual usuarioActual, MpPersonaconocimiento bean)
        {
            return MpPersonaconocimientoDao.actualizar(usuarioActual, bean);
        }

        public MpPersonaconocimientoPk cambiarestado(MpPersonaconocimientoPk pk)
        {

            MpPersonaconocimiento bean = MpPersonaconocimientoDao.obtenerPorId(pk.obtenerArreglo());

            if(bean.Estado == "A")
            {
                bean.Estado = "I";
            }
            else
            {
                bean.Estado = "A";
            }

            MpPersonaconocimientoDao.actualizar(bean);

            return pk;
        }

        public List<DtoTabla> listado(int? pIdPersona)
        {
            return MpPersonaconocimientoDao.listado(pIdPersona);
        }

        public void eliminar(MpPersonaconocimiento bean)
        {
            MpPersonaconocimientoDao.eliminar(bean);     
        }
    }
}
