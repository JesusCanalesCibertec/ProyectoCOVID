using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.programasocial.dao.impl
{
public class PsAtencionTratamientoDaoImpl : GenericoDaoImpl<PsAtencionTratamiento>, PsAtencionTratamientoDao 
{
        private IServiceProvider servicioProveedor;


        public PsAtencionTratamientoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"psatenciontratamiento")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsAtencionTratamiento obtenerPorId(PsAtencionTratamientoPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsAtencionTratamiento obtenerPorId(Nullable<Int32> pIdAtencion,Nullable<Int32> pIdDiagnostico, String pIdTratamiento)
        {
            return obtenerPorId(new PsAtencionTratamientoPk(pIdAtencion, pIdDiagnostico, pIdTratamiento));
        }

        public PsAtencionTratamiento coreInsertar(UsuarioActual usuarioActual, PsAtencionTratamiento bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsAtencionTratamiento coreActualizar(UsuarioActual usuarioActual, PsAtencionTratamiento bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pIdAtencion, Nullable<Int32> pIdDiagnostico, String pIdTratamiento)
        {
            coreEliminar(new PsAtencionTratamientoPk(  pIdAtencion, pIdDiagnostico, pIdTratamiento));
        }

        public void coreEliminar(PsAtencionTratamientoPk id)
        {
            PsAtencionTratamiento bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsAtencionTratamiento coreAnular(UsuarioActual usuarioActual,PsAtencionTratamientoPk id)
        {
            PsAtencionTratamiento bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public PsAtencionTratamiento coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdAtencion, Nullable<Int32> pIdDiagnostico, String pIdTratamiento)
        {
            return coreAnular(usuarioActual,new PsAtencionTratamientoPk( pIdAtencion, pIdDiagnostico, pIdTratamiento));
        }

    }
}
