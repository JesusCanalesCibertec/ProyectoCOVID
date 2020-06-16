using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.dao;
using net.royal.spring.servicio;
using net.royal.spring.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.servicio.impl
{

public class UbicaciongeograficaServicioImpl : GenericoServicioImpl, UbicaciongeograficaServicio {

        private IServiceProvider servicioProveedor;
        private UbicaciongeograficaDao ubicaciongeograficaDao;

        public UbicaciongeograficaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            ubicaciongeograficaDao = servicioProveedor.GetService<UbicaciongeograficaDao>();
        }

        public Ubicaciongeografica coreInsertar(UsuarioActual usuarioActual, Ubicaciongeografica bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return ubicaciongeograficaDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public Ubicaciongeografica coreActualizar(UsuarioActual usuarioActual, Ubicaciongeografica bean)
        {
            return ubicaciongeograficaDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public Ubicaciongeografica coreAnular(UsuarioActual usuarioActual, UbicaciongeograficaPk id)
        {
            return ubicaciongeograficaDao.coreAnular(usuarioActual,id);
        }

        public Ubicaciongeografica coreAnular(UsuarioActual usuarioActual, String pUbigeo)
        {
            return ubicaciongeograficaDao.coreAnular(usuarioActual,  pUbigeo);
        }

        public void coreEliminar(UbicaciongeograficaPk id)
        {
            ubicaciongeograficaDao.coreEliminar(id);
        }

        public void coreEliminar(String pUbigeo)
        {
            ubicaciongeograficaDao.coreEliminar( pUbigeo);
        }


        public Ubicaciongeografica obtenerPorId(UbicaciongeograficaPk id)
        {
            return ubicaciongeograficaDao.obtenerPorId(id);
        }

        public Ubicaciongeografica obtenerPorId(String pUbigeo)
        {
            return ubicaciongeograficaDao.obtenerPorId( pUbigeo);
        }

    }
}
