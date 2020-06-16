using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using System.Collections.Generic;

namespace net.royal.spring.rrhh.dao.impl
{
    public class HrCapacitacionBeneficiarioDaoImpl : GenericoDaoImpl<HrCapacitacionBeneficiario>, HrCapacitacionBeneficiarioDao
    {
        private IServiceProvider servicioProveedor;


        public HrCapacitacionBeneficiarioDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrcapacitacionbeneficiario")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrCapacitacionBeneficiario obtenerPorId(HrCapacitacionBeneficiarioPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrCapacitacionBeneficiario obtenerPorId(String pCompanyowner, String pCapacitacion, String pIdInstitucion, Nullable<Int32> pIdBeneficiario)
        {
            return obtenerPorId(new HrCapacitacionBeneficiarioPk(pCompanyowner, pCapacitacion, pIdInstitucion, pIdBeneficiario));
        }

        public HrCapacitacionBeneficiario coreInsertar(UsuarioActual usuarioActual, HrCapacitacionBeneficiario bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrCapacitacionBeneficiario coreActualizar(UsuarioActual usuarioActual, HrCapacitacionBeneficiario bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pCompanyowner, String pCapacitacion, String pIdInstitucion, Nullable<Int32> pIdBeneficiario)
        {
            coreEliminar(new HrCapacitacionBeneficiarioPk(pCompanyowner, pCapacitacion, pIdInstitucion, pIdBeneficiario));
        }

        public void coreEliminar(HrCapacitacionBeneficiarioPk id)
        {
            HrCapacitacionBeneficiario bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrCapacitacionBeneficiario coreAnular(UsuarioActual usuarioActual, HrCapacitacionBeneficiarioPk id)
        {
            HrCapacitacionBeneficiario bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public HrCapacitacionBeneficiario coreAnular(UsuarioActual usuarioActual, String pCompanyowner, String pCapacitacion, String pIdInstitucion, Nullable<Int32> pIdBeneficiario)
        {
            return coreAnular(usuarioActual, new HrCapacitacionBeneficiarioPk(pCompanyowner, pCapacitacion, pIdInstitucion, pIdBeneficiario));
        }

        public List<HrCapacitacionBeneficiario> listarPorCapacitacion(HrCapacitacionPk hrCapacitacionPk)
        {
            IQueryable<HrCapacitacionBeneficiario> query = getCriteria();
            query = query.Where(x => x.Companyowner == hrCapacitacionPk.Companyowner && x.Capacitacion == hrCapacitacionPk.Capacitacion);
            return query.ToList();
        }
    }
}
