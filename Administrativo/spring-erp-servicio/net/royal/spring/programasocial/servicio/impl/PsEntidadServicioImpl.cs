using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.programasocial.servicio.impl
{

public class PsEntidadServicioImpl : GenericoServicioImpl, PsEntidadServicio {

        private IServiceProvider servicioProveedor;
        private PsEntidadDao psEntidadDao;

        public PsEntidadServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psEntidadDao = servicioProveedor.GetService<PsEntidadDao>();
        }

        public PsEntidad coreInsertar(UsuarioActual usuarioActual, PsEntidad bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return psEntidadDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public PsEntidad coreActualizar(UsuarioActual usuarioActual, PsEntidad bean)
        {
            return psEntidadDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public PsEntidad coreAnular(UsuarioActual usuarioActual, PsEntidadPk id)
        {
            return psEntidadDao.coreAnular(usuarioActual,id);
        }

        public PsEntidad coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdEntidad)
        {
            return psEntidadDao.coreAnular(usuarioActual,  pIdEntidad);
        }

        public void coreEliminar(PsEntidadPk id)
        {
            psEntidadDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad)
        {
            psEntidadDao.coreEliminar( pIdEntidad);
        }


        public PsEntidad obtenerPorId(PsEntidadPk id)
        {
            return psEntidadDao.obtenerPorId(id);
        }

        public PsEntidad obtenerPorId(Nullable<Int32> pIdEntidad)
        {
            return psEntidadDao.obtenerPorId( pIdEntidad);
        }

    }
}
