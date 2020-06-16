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
    public class PsBeneficiarioIngresoCausalDaoImpl : GenericoDaoImpl<PsBeneficiarioIngresoCausal>, PsBeneficiarioIngresoCausalDao
    {
        private IServiceProvider servicioProveedor;


        public PsBeneficiarioIngresoCausalDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psbeneficiarioingresocausal")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PsBeneficiarioIngresoCausal obtenerPorId(PsBeneficiarioIngresoCausalPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsBeneficiarioIngresoCausal obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdIngreso, String pIdCausal)
        {
            return obtenerPorId(new PsBeneficiarioIngresoCausalPk(pIdInstitucion, pIdBeneficiario, pIdIngreso, pIdCausal));
        }

        public PsBeneficiarioIngresoCausal coreInsertar(UsuarioActual usuarioActual, PsBeneficiarioIngresoCausal bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsBeneficiarioIngresoCausal coreActualizar(UsuarioActual usuarioActual, PsBeneficiarioIngresoCausal bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdIngreso, String pIdCausal)
        {
            coreEliminar(new PsBeneficiarioIngresoCausalPk(pIdInstitucion, pIdBeneficiario, pIdIngreso, pIdCausal));
        }

        public void coreEliminar(PsBeneficiarioIngresoCausalPk id)
        {
            PsBeneficiarioIngresoCausal bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsBeneficiarioIngresoCausal coreAnular(UsuarioActual usuarioActual, PsBeneficiarioIngresoCausalPk id)
        {
            PsBeneficiarioIngresoCausal bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public PsBeneficiarioIngresoCausal coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdIngreso, String pIdCausal)
        {
            return coreAnular(usuarioActual, new PsBeneficiarioIngresoCausalPk(pIdInstitucion, pIdBeneficiario, pIdIngreso, pIdCausal));
        }

        public List<PsBeneficiarioIngresoCausal> listarCausal(string institucion, int idBeneficiario, int idUltimoIngreso)
        {
            IQueryable<PsBeneficiarioIngresoCausal> cri = getCriteria();
            cri = cri.Where(x => x.IdBeneficiario == idBeneficiario);
            cri = cri.Where(x => x.IdInstitucion == institucion);
            cri = cri.Where(x => x.IdIngreso == idUltimoIngreso);
            return cri.ToList();
        }


    }
}
