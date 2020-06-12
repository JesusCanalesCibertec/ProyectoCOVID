using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.core.servicio.impl
{

public class UnidadesmastServicioImpl : GenericoServicioImpl, UnidadesmastServicio {

        private IServiceProvider servicioProveedor;
        private UnidadesmastDao unidadesmastDao;

        public UnidadesmastServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            unidadesmastDao = servicioProveedor.GetService<UnidadesmastDao>();
        }

        public Unidadesmast coreInsertar(UsuarioActual usuarioActual, Unidadesmast bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return unidadesmastDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public Unidadesmast coreActualizar(UsuarioActual usuarioActual, Unidadesmast bean)
        {
            return unidadesmastDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public Unidadesmast coreAnular(UsuarioActual usuarioActual, UnidadesmastPk id)
        {
            return unidadesmastDao.coreAnular(usuarioActual,id);
        }

        public Unidadesmast coreAnular(UsuarioActual usuarioActual, String pUnidadcodigo)
        {
            return unidadesmastDao.coreAnular(usuarioActual,  pUnidadcodigo);
        }

        public void coreEliminar(UnidadesmastPk id)
        {
            unidadesmastDao.coreEliminar(id);
        }

        public void coreEliminar(String pUnidadcodigo)
        {
            unidadesmastDao.coreEliminar( pUnidadcodigo);
        }


        public Unidadesmast obtenerPorId(UnidadesmastPk id)
        {
            return unidadesmastDao.obtenerPorId(id);
        }

        public Unidadesmast obtenerPorId(String pUnidadcodigo)
        {
            return unidadesmastDao.obtenerPorId( pUnidadcodigo);
        }

        public List<Unidadesmast> listarTodos()
        {
            return unidadesmastDao.listarTodos();
        }
    }
}
