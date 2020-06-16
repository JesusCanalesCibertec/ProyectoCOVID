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
public class HrCapacitacionempcalDaoImpl : GenericoDaoImpl<HrCapacitacionempcal>, HrCapacitacionempcalDao 
{
        private IServiceProvider servicioProveedor;


        public HrCapacitacionempcalDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"hrcapacitacionempcal")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public HrCapacitacionempcal obtenerPorId(HrCapacitacionempcalPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrCapacitacionempcal obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion)
        {
            return obtenerPorId(new HrCapacitacionempcalPk( pCompanyowner, pCapacitacion, pEmpleado, pEvaluacion));
        }

        public HrCapacitacionempcal coreInsertar(UsuarioActual usuarioActual, HrCapacitacionempcal bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrCapacitacionempcal coreActualizar(UsuarioActual usuarioActual, HrCapacitacionempcal bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion)
        {
            coreEliminar(new HrCapacitacionempcalPk( pCompanyowner, pCapacitacion, pEmpleado, pEvaluacion));
        }

        public void coreEliminar(HrCapacitacionempcalPk id)
        {
            HrCapacitacionempcal bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public HrCapacitacionempcal coreAnular(UsuarioActual usuarioActual,HrCapacitacionempcalPk id)
        {
            HrCapacitacionempcal bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public HrCapacitacionempcal coreAnular(UsuarioActual usuarioActual,String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion)
        {
            return coreAnular(usuarioActual,new HrCapacitacionempcalPk( pCompanyowner, pCapacitacion, pEmpleado, pEvaluacion));
        }

    }
}
