using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.sistema.dao.impl
{
public class SyPreferencesDaoImpl : GenericoDaoImpl<SyPreferences>, SyPreferencesDao 
{
        private IServiceProvider servicioProveedor;


        public SyPreferencesDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"sypreferences")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public SyPreferences obtenerPorId(SyPreferencesPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public SyPreferences obtenerPorId(String pUsuario,String pPreference)
        {
            return obtenerPorId(new SyPreferencesPk( pUsuario, pPreference));
        }

        public SyPreferences coreInsertar(UsuarioActual usuarioActual, SyPreferences bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public SyPreferences coreActualizar(UsuarioActual usuarioActual, SyPreferences bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pUsuario,String pPreference)
        {
            coreEliminar(new SyPreferencesPk( pUsuario, pPreference));
        }

        public void coreEliminar(SyPreferencesPk id)
        {
            SyPreferences bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public SyPreferences coreAnular(UsuarioActual usuarioActual,SyPreferencesPk id)
        {
            SyPreferences bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        public SyPreferences coreAnular(UsuarioActual usuarioActual,String pUsuario,String pPreference)
        {
            return coreAnular(usuarioActual,new SyPreferencesPk( pUsuario, pPreference));
        }

        public String obtenerPorIdCadena(String pUsuario, String pPreference) {
            SyPreferences pre = this.obtenerPorId(pUsuario, pPreference);
            if (pre != null)
                return UString.trimNotNull(pre.Valorstring);
            return "";
        }

        public SyPreferences guardarCadena(UsuarioActual usuarioActual, String pUsuario, String pPreference, String valor)
        {
            return guardarCadena(usuarioActual, pUsuario, pPreference, valor,null);
        }

        public SyPreferences guardarCadena(UsuarioActual usuarioActual, String pUsuario, String pPreference, String valor, String aplicacionCodigo) {
            SyPreferences pre = this.obtenerPorId(pUsuario, pPreference);
            if (pre == null) {
                pre = new SyPreferences();
                pre.Usuario = pUsuario;
                pre.Preference = pPreference;
                pre.Tipovalor = "C";
                pre.Valorstring = valor;
                pre.Aplicacioncodigo = aplicacionCodigo;
                pre = this.coreInsertar(usuarioActual, pre);
            } else {
                pre.Valorstring = valor;
                pre = this.coreActualizar(usuarioActual,pre);
            }
            return pre;
        }

    }
}
