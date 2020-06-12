using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.core.dao.impl
{
public class UnidadesmastDaoImpl : GenericoDaoImpl<Unidadesmast>, UnidadesmastDao 
{
        private IServiceProvider servicioProveedor;


        public UnidadesmastDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"unidadesmast")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public Unidadesmast obtenerPorId(UnidadesmastPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public Unidadesmast obtenerPorId(String pUnidadcodigo)
        {
            return obtenerPorId(new UnidadesmastPk( pUnidadcodigo));
        }

        public Unidadesmast coreInsertar(UsuarioActual usuarioActual, Unidadesmast bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public Unidadesmast coreActualizar(UsuarioActual usuarioActual, Unidadesmast bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pUnidadcodigo)
        {
            coreEliminar(new UnidadesmastPk( pUnidadcodigo));
        }

        public void coreEliminar(UnidadesmastPk id)
        {
            Unidadesmast bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public Unidadesmast coreAnular(UsuarioActual usuarioActual,UnidadesmastPk id)
        {
            Unidadesmast bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public Unidadesmast coreAnular(UsuarioActual usuarioActual,String pUnidadcodigo)
        {
            return coreAnular(usuarioActual,new UnidadesmastPk( pUnidadcodigo));
        }

    }
}
