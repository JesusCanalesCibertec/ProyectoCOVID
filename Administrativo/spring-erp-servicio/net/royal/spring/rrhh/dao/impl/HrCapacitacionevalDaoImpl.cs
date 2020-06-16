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
public class HrCapacitacionevalDaoImpl : GenericoDaoImpl<HrCapacitacioneval>, HrCapacitacionevalDao 
{
        private IServiceProvider servicioProveedor;


        public HrCapacitacionevalDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"hrcapacitacioneval")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public HrCapacitacioneval obtenerPorId(HrCapacitacionevalPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrCapacitacioneval obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion)
        {
            return obtenerPorId(new HrCapacitacionevalPk( pCompanyowner, pCapacitacion, pEvaluacion));
        }

        public HrCapacitacioneval coreInsertar(UsuarioActual usuarioActual, HrCapacitacioneval bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrCapacitacioneval coreActualizar(UsuarioActual usuarioActual, HrCapacitacioneval bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion)
        {
            coreEliminar(new HrCapacitacionevalPk( pCompanyowner, pCapacitacion, pEvaluacion));
        }

        public void coreEliminar(HrCapacitacionevalPk id)
        {
            HrCapacitacioneval bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public HrCapacitacioneval coreAnular(UsuarioActual usuarioActual,HrCapacitacionevalPk id)
        {
            HrCapacitacioneval bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public HrCapacitacioneval coreAnular(UsuarioActual usuarioActual,String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion)
        {
            return coreAnular(usuarioActual,new HrCapacitacionevalPk( pCompanyowner, pCapacitacion, pEvaluacion));
        }

    }
}
