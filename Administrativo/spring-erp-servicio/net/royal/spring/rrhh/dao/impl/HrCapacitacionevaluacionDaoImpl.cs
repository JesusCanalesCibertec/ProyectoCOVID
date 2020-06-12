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
public class HrCapacitacionevaluacionDaoImpl : GenericoDaoImpl<HrCapacitacionevaluacion>, HrCapacitacionevaluacionDao 
{
        private IServiceProvider servicioProveedor;


        public HrCapacitacionevaluacionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"hrcapacitacionevaluacion")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public HrCapacitacionevaluacion obtenerPorId(HrCapacitacionevaluacionPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrCapacitacionevaluacion obtenerPorId(String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor)
        {
            return obtenerPorId(new HrCapacitacionevaluacionPk( pCapacitacion, pSecuenciaempleado, pFactor));
        }

        public HrCapacitacionevaluacion coreInsertar(UsuarioActual usuarioActual, HrCapacitacionevaluacion bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrCapacitacionevaluacion coreActualizar(UsuarioActual usuarioActual, HrCapacitacionevaluacion bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor)
        {
            coreEliminar(new HrCapacitacionevaluacionPk( pCapacitacion, pSecuenciaempleado, pFactor));
        }

        public void coreEliminar(HrCapacitacionevaluacionPk id)
        {
            HrCapacitacionevaluacion bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public HrCapacitacionevaluacion coreAnular(UsuarioActual usuarioActual,HrCapacitacionevaluacionPk id)
        {
            HrCapacitacionevaluacion bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public HrCapacitacionevaluacion coreAnular(UsuarioActual usuarioActual,String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor)
        {
            return coreAnular(usuarioActual,new HrCapacitacionevaluacionPk( pCapacitacion, pSecuenciaempleado, pFactor));
        }

    }
}
