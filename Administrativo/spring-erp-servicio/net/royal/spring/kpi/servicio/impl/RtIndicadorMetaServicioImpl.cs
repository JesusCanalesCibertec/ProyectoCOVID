using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.kpi.dao;
using net.royal.spring.kpi.servicio;
using net.royal.spring.kpi.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.kpi.servicio.impl
{

public class RtIndicadorMetaServicioImpl : GenericoServicioImpl, RtIndicadorMetaServicio {

        private IServiceProvider servicioProveedor;
        private RtIndicadorMetaDao rtIndicadorMetaDao;

        public RtIndicadorMetaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            rtIndicadorMetaDao = servicioProveedor.GetService<RtIndicadorMetaDao>();
        }

        public RtIndicadorMeta coreInsertar(UsuarioActual usuarioActual, RtIndicadorMeta bean)
        {
            bean.IdMeta = rtIndicadorMetaDao.generarCodigo();
            return rtIndicadorMetaDao.coreInsertar(usuarioActual,bean);
        }

        public RtIndicadorMeta coreActualizar(UsuarioActual usuarioActual, RtIndicadorMeta bean)
        {
            return rtIndicadorMetaDao.coreActualizar(usuarioActual,bean);
        }

        public RtIndicadorMeta coreAnular(UsuarioActual usuarioActual, RtIndicadorMetaPk id)
        {
            return rtIndicadorMetaDao.coreAnular(usuarioActual, id);
        }

        public RtIndicadorMeta coreAnular(UsuarioActual usuarioActual, String pIdIndicador, Nullable<Int32> pIdMeta)
        {
            return rtIndicadorMetaDao.coreAnular(usuarioActual, pIdIndicador, pIdMeta);
        }


        public void coreEliminar(RtIndicadorMetaPk id)
        {
            rtIndicadorMetaDao.coreEliminar(id);
        }



        public void coreEliminar(String pIdIndicador,Nullable<Int32> pIdMeta)
        {
            rtIndicadorMetaDao.coreEliminar( pIdIndicador, pIdMeta);
        }


        public RtIndicadorMeta obtenerPorId(RtIndicadorMetaPk id)
        {
            return rtIndicadorMetaDao.obtenerPorId(id.obtenerArreglo());
        }

        public RtIndicadorMeta obtenerPorId(String pIdIndicador,Nullable<Int32> pIdMeta)
        {
            return rtIndicadorMetaDao.obtenerPorId( pIdIndicador, pIdMeta);
        }

        public List<RtIndicadorMeta> listado(string pIdIndicador)
        {
            return rtIndicadorMetaDao.listado(pIdIndicador);
        }
    }
}
