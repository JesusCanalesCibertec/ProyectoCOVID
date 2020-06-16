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
    public class HrEmpleadocapacitacionDaoImpl : GenericoDaoImpl<HrEmpleadocapacitacion>, HrEmpleadocapacitacionDao
    {
        private IServiceProvider servicioProveedor;


        public HrEmpleadocapacitacionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrempleadocapacitacion")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrEmpleadocapacitacion obtenerPorId(HrEmpleadocapacitacionPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrEmpleadocapacitacion obtenerPorId(String pCompanyowner, String pCapacitacion, Nullable<Int32> pEmpleado)
        {
            return obtenerPorId(new HrEmpleadocapacitacionPk(pCompanyowner, pCapacitacion, pEmpleado));
        }

        public HrEmpleadocapacitacion coreInsertar(UsuarioActual usuarioActual, HrEmpleadocapacitacion bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrEmpleadocapacitacion coreActualizar(UsuarioActual usuarioActual, HrEmpleadocapacitacion bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pCompanyowner, String pCapacitacion, Nullable<Int32> pEmpleado)
        {
            coreEliminar(new HrEmpleadocapacitacionPk(pCompanyowner, pCapacitacion, pEmpleado));
        }

        public void coreEliminar(HrEmpleadocapacitacionPk id)
        {
            HrEmpleadocapacitacion bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrEmpleadocapacitacion coreAnular(UsuarioActual usuarioActual, HrEmpleadocapacitacionPk id)
        {
            HrEmpleadocapacitacion bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public HrEmpleadocapacitacion coreAnular(UsuarioActual usuarioActual, String pCompanyowner, String pCapacitacion, Nullable<Int32> pEmpleado)
        {
            return coreAnular(usuarioActual, new HrEmpleadocapacitacionPk(pCompanyowner, pCapacitacion, pEmpleado));
        }

        public List<HrEmpleadocapacitacion> listarPorCapacitacion(HrCapacitacionPk hrCapacitacionPk)
        {
            IQueryable<HrEmpleadocapacitacion> query = this.getCriteria();

            query = query.Where(p => p.Companyowner == hrCapacitacionPk.Companyowner);
            query = query.Where(p => p.Capacitacion == hrCapacitacionPk.Capacitacion);
            query = query.OrderBy(p => p.Empleado);

            return query.ToList();

        }

    }
}
