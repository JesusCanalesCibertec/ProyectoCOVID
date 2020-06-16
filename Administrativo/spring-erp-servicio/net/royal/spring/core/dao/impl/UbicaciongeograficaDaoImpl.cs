using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.dao;
using net.royal.spring.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.dao.impl
{
public class UbicaciongeograficaDaoImpl : GenericoDaoImpl<Ubicaciongeografica>, UbicaciongeograficaDao 
{
        private IServiceProvider servicioProveedor;


        public UbicaciongeograficaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"ubicaciongeografica")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public Ubicaciongeografica obtenerPorId(UbicaciongeograficaPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public Ubicaciongeografica obtenerPorId(String pUbigeo)
        {
            return obtenerPorId(new UbicaciongeograficaPk( pUbigeo));
        }

        public Ubicaciongeografica coreInsertar(UsuarioActual usuarioActual, Ubicaciongeografica bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public Ubicaciongeografica coreActualizar(UsuarioActual usuarioActual, Ubicaciongeografica bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pUbigeo)
        {
            coreEliminar(new UbicaciongeograficaPk( pUbigeo));
        }

        public void coreEliminar(UbicaciongeograficaPk id)
        {
            Ubicaciongeografica bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public Ubicaciongeografica coreAnular(UsuarioActual usuarioActual,UbicaciongeograficaPk id)
        {
            Ubicaciongeografica bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public Ubicaciongeografica coreAnular(UsuarioActual usuarioActual,String pUbigeo)
        {
            return coreAnular(usuarioActual,new UbicaciongeograficaPk( pUbigeo));
        }

    }
}
