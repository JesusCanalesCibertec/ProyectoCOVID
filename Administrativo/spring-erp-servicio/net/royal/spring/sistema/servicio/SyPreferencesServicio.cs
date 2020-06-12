using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema.servicio
{

    public interface SyPreferencesServicio : GenericoServicio {
        SyPreferences obtenerPorId(SyPreferencesPk id);
        SyPreferences obtenerPorId(String pUsuario,String pPreference);
        SyPreferences coreActualizar(UsuarioActual usuarioActual, SyPreferences bean);
        SyPreferences coreInsertar(UsuarioActual usuarioActual, SyPreferences bean);
        void coreEliminar(SyPreferencesPk id);
        void coreEliminar(String pUsuario,String pPreference);
    }
}
