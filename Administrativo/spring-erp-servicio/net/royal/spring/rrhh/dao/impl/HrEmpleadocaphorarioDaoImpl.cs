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
    public class HrEmpleadocaphorarioDaoImpl : GenericoDaoImpl<HrEmpleadocaphorario>, HrEmpleadocaphorarioDao
    {
        private IServiceProvider servicioProveedor;


        public HrEmpleadocaphorarioDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrempleadocaphorario")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrEmpleadocaphorario obtenerPorId(HrEmpleadocaphorarioPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrEmpleadocaphorario obtenerPorId(String pCompanyowner, String pCapacitacion, Nullable<Int32> pEmpleado, Nullable<Int32> pSecuencia)
        {
            return obtenerPorId(new HrEmpleadocaphorarioPk(pCompanyowner, pCapacitacion, pEmpleado, pSecuencia));
        }

        public HrEmpleadocaphorario coreInsertar(UsuarioActual usuarioActual, HrEmpleadocaphorario bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrEmpleadocaphorario coreActualizar(UsuarioActual usuarioActual, HrEmpleadocaphorario bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pCompanyowner, String pCapacitacion, Nullable<Int32> pEmpleado, Nullable<Int32> pSecuencia)
        {
            coreEliminar(new HrEmpleadocaphorarioPk(pCompanyowner, pCapacitacion, pEmpleado, pSecuencia));
        }

        public void coreEliminar(HrEmpleadocaphorarioPk id)
        {
            HrEmpleadocaphorario bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrEmpleadocaphorario coreAnular(UsuarioActual usuarioActual, HrEmpleadocaphorarioPk id)
        {
            HrEmpleadocaphorario bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public HrEmpleadocaphorario coreAnular(UsuarioActual usuarioActual, String pCompanyowner, String pCapacitacion, Nullable<Int32> pEmpleado, Nullable<Int32> pSecuencia)
        {
            return coreAnular(usuarioActual, new HrEmpleadocaphorarioPk(pCompanyowner, pCapacitacion, pEmpleado, pSecuencia));
        }

        public List<HrEmpleadocaphorario> listarPorCapacitacion(HrCapacitacionPk hrCapacitacionPk)
        {
            IQueryable<HrEmpleadocaphorario> query = this.getCriteria();

            query = query.Where(p => p.Companyowner == hrCapacitacionPk.Companyowner);
            query = query.Where(p => p.Capacitacion == hrCapacitacionPk.Capacitacion);
            query = query.OrderBy(p => p.Empleado);
            query = query.OrderBy(p => p.Secuencia);

            return query.ToList();
        }

    }
}
