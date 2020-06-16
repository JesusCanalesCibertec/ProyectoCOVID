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
public class PsSaludTerapiaDaoImpl : GenericoDaoImpl<PsSaludTerapia>, PsSaludTerapiaDao 
{
        private IServiceProvider servicioProveedor;


        public PsSaludTerapiaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"pssaludterapia")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsSaludTerapia obtenerPorId(PsSaludTerapiaPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsSaludTerapia obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTerapia)
        {
            return obtenerPorId(new PsSaludTerapiaPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTerapia));
        }

        public PsSaludTerapia coreInsertar(UsuarioActual usuarioActual, PsSaludTerapia bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsSaludTerapia coreActualizar(UsuarioActual usuarioActual, PsSaludTerapia bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTerapia)
        {
            coreEliminar(new PsSaludTerapiaPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTerapia));
        }

        public void coreEliminar(PsSaludTerapiaPk id)
        {
            PsSaludTerapia bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsSaludTerapia coreAnular(UsuarioActual usuarioActual,PsSaludTerapiaPk id)
        {
            PsSaludTerapia bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        public PsSaludTerapia coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTerapia)
        {
            return coreAnular(usuarioActual,new PsSaludTerapiaPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTerapia));
        }

        public List<PsSaludTerapia> obtenerListado(string pIdInstitucion, int? pIdBeneficiario, int? pIdSalud) {
            IQueryable<PsSaludTerapia> queryable = getCriteria();
            queryable = queryable.Where(x => x.IdBeneficiario == pIdBeneficiario);
            queryable = queryable.Where(x => x.IdInstitucion == pIdInstitucion);
            queryable = queryable.Where(x => x.IdSalud == pIdSalud);

            return queryable.ToList();
        }
    }
}
