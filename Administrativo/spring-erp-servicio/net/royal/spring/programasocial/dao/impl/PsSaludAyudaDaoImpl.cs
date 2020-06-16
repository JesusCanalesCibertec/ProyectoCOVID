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
public class PsSaludAyudaDaoImpl : GenericoDaoImpl<PsSaludAyuda>, PsSaludAyudaDao
    {
        private IServiceProvider servicioProveedor;


        public PsSaludAyudaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"pssaludayuda")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsSaludAyuda obtenerPorId(PsSaludAyudaPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsSaludAyuda obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdEstado)
        {
            return obtenerPorId(new PsSaludDiscapacidadPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdEstado));
        }

        public PsSaludAyuda coreInsertar(UsuarioActual usuarioActual, PsSaludAyuda bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsSaludAyuda coreActualizar(UsuarioActual usuarioActual, PsSaludAyuda bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdEstado)
        {
            coreEliminar(new PsSaludAyudaPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdEstado));
        }

        public void coreEliminar(PsSaludAyudaPk id)
        {
            PsSaludAyuda bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsSaludAyuda coreAnular(UsuarioActual usuarioActual, PsSaludAyudaPk id)
        {
            PsSaludAyuda bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        public PsSaludAyuda coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdEstado)
        {
            return coreAnular(usuarioActual,new PsSaludAyudaPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdEstado));
        }

        public List<PsSaludAyuda> obtenerListado(string pIdInstitucion, int? pIdBeneficiario, int? pIdSalud) {
            IQueryable<PsSaludAyuda> queryable = getCriteria();
            queryable = queryable.Where(x => x.IdBeneficiario == pIdBeneficiario);
            queryable = queryable.Where(x => x.IdInstitucion == pIdInstitucion);
            queryable = queryable.Where(x => x.IdSalud == pIdSalud);
         
            return  queryable.ToList();
        }
    }
}
