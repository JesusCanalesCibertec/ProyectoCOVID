using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.sistema.servicio.impl
{

public class SyPreferencesServicioImpl : GenericoServicioImpl, SyPreferencesServicio {

        private IServiceProvider servicioProveedor;
        private SyPreferencesDao syPreferencesDao;

        public SyPreferencesServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            syPreferencesDao = servicioProveedor.GetService<SyPreferencesDao>();
        }

        public SyPreferences coreInsertar(UsuarioActual usuarioActual, SyPreferences bean)
        {
            return syPreferencesDao.coreInsertar(usuarioActual,bean);
        }

        public SyPreferences coreActualizar(UsuarioActual usuarioActual, SyPreferences bean)
        {
            return syPreferencesDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(SyPreferencesPk id)
        {
            syPreferencesDao.coreEliminar(id);
        }

        public void coreEliminar(String pUsuario,String pPreference)
        {
            syPreferencesDao.coreEliminar( pUsuario, pPreference);
        }


        public SyPreferences obtenerPorId(SyPreferencesPk id)
        {
            return syPreferencesDao.obtenerPorId(id);
        }

        public SyPreferences obtenerPorId(String pUsuario,String pPreference)
        {
            return syPreferencesDao.obtenerPorId( pUsuario, pPreference);
        }

    }
}
