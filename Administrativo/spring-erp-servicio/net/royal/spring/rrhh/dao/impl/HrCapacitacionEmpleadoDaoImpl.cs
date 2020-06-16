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
    public class HrCapacitacionEmpleadoDaoImpl : GenericoDaoImpl<HrCapacitacionEmpleado>, HrCapacitacionEmpleadoDao
    {
        private IServiceProvider servicioProveedor;


        public HrCapacitacionEmpleadoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrcapacitacionempleado")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrCapacitacionEmpleado obtenerPorId(HrCapacitacionEmpleadoPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrCapacitacionEmpleado obtenerPorId(String pCompanyowner, String pCapacitacion, String pIdInstitucion, Nullable<Int32> pIdEmpleado)
        {
            return obtenerPorId(new HrCapacitacionEmpleadoPk(pCompanyowner, pCapacitacion, pIdInstitucion, pIdEmpleado));
        }

        public HrCapacitacionEmpleado coreInsertar(UsuarioActual usuarioActual, HrCapacitacionEmpleado bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrCapacitacionEmpleado coreActualizar(UsuarioActual usuarioActual, HrCapacitacionEmpleado bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pCompanyowner, String pCapacitacion, String pIdInstitucion, Nullable<Int32> pIdEmpleado)
        {
            coreEliminar(new HrCapacitacionEmpleadoPk(pCompanyowner, pCapacitacion, pIdInstitucion, pIdEmpleado));
        }

        public void coreEliminar(HrCapacitacionEmpleadoPk id)
        {
            HrCapacitacionEmpleado bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrCapacitacionEmpleado coreAnular(UsuarioActual usuarioActual, HrCapacitacionEmpleadoPk id)
        {
            HrCapacitacionEmpleado bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public HrCapacitacionEmpleado coreAnular(UsuarioActual usuarioActual, String pCompanyowner, String pCapacitacion, String pIdInstitucion, Nullable<Int32> pIdEmpleado)
        {
            return coreAnular(usuarioActual, new HrCapacitacionEmpleadoPk(pCompanyowner, pCapacitacion, pIdInstitucion, pIdEmpleado));
        }

        public List<HrCapacitacionEmpleado> listarPorCapacitacion(HrCapacitacionPk hrCapacitacionPk)
        {
            IQueryable<HrCapacitacionEmpleado> query = getCriteria();
            query = query.Where(x => x.Companyowner == hrCapacitacionPk.Companyowner && x.Capacitacion == hrCapacitacionPk.Capacitacion);
            return query.ToList();
        }
    }
}
