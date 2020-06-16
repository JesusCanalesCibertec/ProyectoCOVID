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
public class PsSaludSubcondicionDaoImpl : GenericoDaoImpl<PsSaludSubcondicion>, PsSaludSubcondicionDao 
{
        private IServiceProvider servicioProveedor;


        public PsSaludSubcondicionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"pssaludsubcondicion")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsSaludSubcondicion obtenerPorId(PsSaludSubcondicionPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsSaludSubcondicion obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdCondicion,String pIdSubcondicion)
        {
            return obtenerPorId(new PsSaludSubcondicionPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdCondicion, pIdSubcondicion));
        }

        public PsSaludSubcondicion coreInsertar(UsuarioActual usuarioActual, PsSaludSubcondicion bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsSaludSubcondicion coreActualizar(UsuarioActual usuarioActual, PsSaludSubcondicion bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdCondicion,String pIdSubcondicion)
        {
            coreEliminar(new PsSaludSubcondicionPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdCondicion, pIdSubcondicion));
        }

        public void coreEliminar(PsSaludSubcondicionPk id)
        {
            PsSaludSubcondicion bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsSaludSubcondicion coreAnular(UsuarioActual usuarioActual,PsSaludSubcondicionPk id)
        {
            PsSaludSubcondicion bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        public PsSaludSubcondicion coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdCondicion,String pIdSubcondicion)
        {
            return coreAnular(usuarioActual,new PsSaludSubcondicionPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdCondicion, pIdSubcondicion));
        }

        public List<PsSaludSubcondicion> obtenerListado(string pIdInstitucion, int? pIdBeneficiario, int? pIdSalud) {
            
                IQueryable<PsSaludSubcondicion> queryable = getCriteria();
                queryable = queryable.Where(x => x.IdBeneficiario == pIdBeneficiario);
                queryable = queryable.Where(x => x.IdInstitucion == pIdInstitucion);
                queryable = queryable.Where(x => x.IdSalud == pIdSalud);

                return queryable.ToList();
            }
        
    }
}
