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
public class PsAtencionDetalleDaoImpl : GenericoDaoImpl<PsAtencionDetalle>, PsAtencionDetalleDao 
{
        private IServiceProvider servicioProveedor;


        public PsAtencionDetalleDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"psatenciondetalle")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsAtencionDetalle obtenerPorId(PsAtencionDetallePk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsAtencionDetalle obtenerPorId(Nullable<Int32> pIdAtencion,Nullable<Int32> pIdDiagnostico)
        {
            return obtenerPorId(new PsAtencionDetallePk(pIdAtencion, pIdDiagnostico));
        }

        public PsAtencionDetalle coreInsertar(UsuarioActual usuarioActual, PsAtencionDetalle bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsAtencionDetalle coreActualizar(UsuarioActual usuarioActual, PsAtencionDetalle bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pIdAtencion,Nullable<Int32> pIdDiagnostico)
        {
            coreEliminar(new PsAtencionDetallePk( pIdAtencion, pIdDiagnostico));
        }

        public void coreEliminar(PsAtencionDetallePk id)
        {
            PsAtencionDetalle bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsAtencionDetalle coreAnular(UsuarioActual usuarioActual,PsAtencionDetallePk id)
        {
            PsAtencionDetalle bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public PsAtencionDetalle coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdAtencion,Nullable<Int32> pIdDetalle)
        {
            return null;
        }

       
    }
}
