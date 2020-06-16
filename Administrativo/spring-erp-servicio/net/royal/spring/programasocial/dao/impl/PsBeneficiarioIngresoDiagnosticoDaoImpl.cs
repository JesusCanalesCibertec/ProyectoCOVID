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
public class PsBeneficiarioIngresoDiagnosticoDaoImpl : GenericoDaoImpl<PsBeneficiarioIngresoDiagnostico>, PsBeneficiarioIngresoDiagnosticoDao 
{
        private IServiceProvider servicioProveedor;


        public PsBeneficiarioIngresoDiagnosticoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"psbeneficiarioingresodiagnostico")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsBeneficiarioIngresoDiagnostico obtenerPorId(PsBeneficiarioIngresoDiagnosticoPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsBeneficiarioIngresoDiagnostico obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso, String pIdDiagnostico)
        {
            return obtenerPorId(new PsBeneficiarioIngresoDiagnosticoPk( pIdInstitucion, pIdBeneficiario, pIdIngreso, pIdDiagnostico));
        }

        public PsBeneficiarioIngresoDiagnostico coreInsertar(UsuarioActual usuarioActual, PsBeneficiarioIngresoDiagnostico bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsBeneficiarioIngresoDiagnostico coreActualizar(UsuarioActual usuarioActual, PsBeneficiarioIngresoDiagnostico bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso, String pIdDiagnostico)
        {
            coreEliminar(new PsBeneficiarioIngresoDiagnosticoPk( pIdInstitucion, pIdBeneficiario, pIdIngreso, pIdDiagnostico));
        }

        public void coreEliminar(PsBeneficiarioIngresoDiagnosticoPk id)
        {
            PsBeneficiarioIngresoDiagnostico bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsBeneficiarioIngresoDiagnostico coreAnular(UsuarioActual usuarioActual,PsBeneficiarioIngresoDiagnosticoPk id)
        {
            PsBeneficiarioIngresoDiagnostico bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        public PsBeneficiarioIngresoDiagnostico coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso, String pIdDiagnostico)
        {
            return coreAnular(usuarioActual,new PsBeneficiarioIngresoDiagnosticoPk( pIdInstitucion, pIdBeneficiario, pIdIngreso, pIdDiagnostico));
        }

    }
}
