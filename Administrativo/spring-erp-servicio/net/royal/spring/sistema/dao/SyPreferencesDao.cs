using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema.dao
{

    public interface SyPreferencesDao : GenericoDao<SyPreferences>
    {
        SyPreferences obtenerPorId(String pUsuario,String pPreference);
        SyPreferences coreActualizar(UsuarioActual usuarioActual, SyPreferences bean);
        SyPreferences coreInsertar(UsuarioActual usuarioActual, SyPreferences bean);
        void coreEliminar(SyPreferencesPk id);
        void coreEliminar(String pUsuario,String pPreference);

        String obtenerPorIdCadena(String pUsuario, String pPreference);
        SyPreferences guardarCadena(UsuarioActual usuarioActual,String pUsuario, String pPreference,String valor);
        SyPreferences guardarCadena(UsuarioActual usuarioActual, String pUsuario, String pPreference, String valor,String aplicacionCodigo);
    }
}