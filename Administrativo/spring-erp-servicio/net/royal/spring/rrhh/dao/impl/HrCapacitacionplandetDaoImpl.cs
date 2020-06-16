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
public class HrCapacitacionplandetDaoImpl : GenericoDaoImpl<HrCapacitacionplandet>, HrCapacitacionplandetDao 
{
        private IServiceProvider servicioProveedor;


        public HrCapacitacionplandetDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"hrcapacitacionplandet")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public HrCapacitacionplandet obtenerPorId(HrCapacitacionplandetPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrCapacitacionplandet obtenerPorId(String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion)
        {
            return obtenerPorId(new HrCapacitacionplandetPk( pCompanyowner, pAnioplan, pCapacitacion));
        }

        public HrCapacitacionplandet coreInsertar(UsuarioActual usuarioActual, HrCapacitacionplandet bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrCapacitacionplandet coreActualizar(UsuarioActual usuarioActual, HrCapacitacionplandet bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion)
        {
            coreEliminar(new HrCapacitacionplandetPk( pCompanyowner, pAnioplan, pCapacitacion));
        }

        public void coreEliminar(HrCapacitacionplandetPk id)
        {
            HrCapacitacionplandet bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public HrCapacitacionplandet coreAnular(UsuarioActual usuarioActual,HrCapacitacionplandetPk id)
        {
            HrCapacitacionplandet bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public HrCapacitacionplandet coreAnular(UsuarioActual usuarioActual,String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion)
        {
            return coreAnular(usuarioActual,new HrCapacitacionplandetPk( pCompanyowner, pAnioplan, pCapacitacion));
        }

    }
}
