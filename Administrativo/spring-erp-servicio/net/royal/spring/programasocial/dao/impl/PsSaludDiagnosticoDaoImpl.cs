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
using System.Collections.Generic;

namespace net.royal.spring.programasocial.dao.impl
{
public class PsSaludDiagnosticoDaoImpl : GenericoDaoImpl<PsSaludDiagnostico>, PsSaludDiagnosticoDao 
{
        private IServiceProvider servicioProveedor;


        public PsSaludDiagnosticoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"pssaluddiagnostico")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsSaludDiagnostico obtenerPorId(PsSaludDiscapacidadPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsSaludDiagnostico obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiagnostico)
        {
            return obtenerPorId(new PsSaludDiscapacidadPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdDiagnostico));
        }

        public PsSaludDiagnostico coreInsertar(UsuarioActual usuarioActual, PsSaludDiagnostico bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsSaludDiagnostico coreActualizar(UsuarioActual usuarioActual, PsSaludDiagnostico bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiagnostico)
        {
            coreEliminar(new PsSaludDiscapacidadPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdDiagnostico));
        }

        public void coreEliminar(PsSaludDiscapacidadPk id)
        {
            PsSaludDiagnostico bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsSaludDiagnostico coreAnular(UsuarioActual usuarioActual,PsSaludDiscapacidadPk id)
        {
            PsSaludDiagnostico bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        public PsSaludDiagnostico coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiagnostico)
        {
            return coreAnular(usuarioActual,new PsSaludDiscapacidadPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdDiagnostico));
        }

        public List<PsSaludDiagnostico> obtenerListado(string pIdInstitucion, int? pIdBeneficiario, int? pIdSalud) {
            IQueryable<PsSaludDiagnostico> queryable = getCriteria();
            queryable = queryable.Where(x => x.IdBeneficiario == pIdBeneficiario);
            queryable = queryable.Where(x => x.IdInstitucion == pIdInstitucion);
            queryable = queryable.Where(x => x.IdSalud == pIdSalud);
         
            return  queryable.ToList();
        }
    }
}
