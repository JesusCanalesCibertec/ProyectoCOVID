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

namespace net.royal.spring.rrhh.dao.impl
{
public class HrEmpleadoasistenciasDaoImpl : GenericoDaoImpl<HrEmpleadoasistencias>, HrEmpleadoasistenciasDao 
{
        private IServiceProvider servicioProveedor;


        public HrEmpleadoasistenciasDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"hrempleadoasistencias")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public HrEmpleadoasistencias obtenerPorId(HrEmpleadoasistenciasPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrEmpleadoasistencias obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            return obtenerPorId(new HrEmpleadoasistenciasPk( pCompanyowner, pCapacitacion, pEmpleado, pSecuencia));
        }

        public HrEmpleadoasistencias coreInsertar(UsuarioActual usuarioActual, HrEmpleadoasistencias bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrEmpleadoasistencias coreActualizar(UsuarioActual usuarioActual, HrEmpleadoasistencias bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            coreEliminar(new HrEmpleadoasistenciasPk( pCompanyowner, pCapacitacion, pEmpleado, pSecuencia));
        }

        public void coreEliminar(HrEmpleadoasistenciasPk id)
        {
            HrEmpleadoasistencias bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public HrEmpleadoasistencias coreAnular(UsuarioActual usuarioActual,HrEmpleadoasistenciasPk id)
        {
            HrEmpleadoasistencias bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        public HrEmpleadoasistencias coreAnular(UsuarioActual usuarioActual,String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            return coreAnular(usuarioActual,new HrEmpleadoasistenciasPk( pCompanyowner, pCapacitacion, pEmpleado, pSecuencia));
        }

    }
}
