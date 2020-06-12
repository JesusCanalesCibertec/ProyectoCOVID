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
public class HrCapacitacionplanDaoImpl : GenericoDaoImpl<HrCapacitacionplan>, HrCapacitacionplanDao 
{
        private IServiceProvider servicioProveedor;


        public HrCapacitacionplanDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"hrcapacitacionplan")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public HrCapacitacionplan obtenerPorId(HrCapacitacionplanPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrCapacitacionplan obtenerPorId(String pCompanyowner,Nullable<Decimal> pAnioplan)
        {
            return obtenerPorId(new HrCapacitacionplanPk( pCompanyowner, pAnioplan));
        }

        public HrCapacitacionplan coreInsertar(UsuarioActual usuarioActual, HrCapacitacionplan bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrCapacitacionplan coreActualizar(UsuarioActual usuarioActual, HrCapacitacionplan bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pCompanyowner,Nullable<Decimal> pAnioplan)
        {
            coreEliminar(new HrCapacitacionplanPk( pCompanyowner, pAnioplan));
        }

        public void coreEliminar(HrCapacitacionplanPk id)
        {
            HrCapacitacionplan bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public HrCapacitacionplan coreAnular(UsuarioActual usuarioActual,HrCapacitacionplanPk id)
        {
            HrCapacitacionplan bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public HrCapacitacionplan coreAnular(UsuarioActual usuarioActual,String pCompanyowner,Nullable<Decimal> pAnioplan)
        {
            return coreAnular(usuarioActual,new HrCapacitacionplanPk( pCompanyowner, pAnioplan));
        }

    }
}
