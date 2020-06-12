using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.rrhh.servicio.impl
{

public class HrCapacitacionplandetServicioImpl : GenericoServicioImpl, HrCapacitacionplandetServicio {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionplandetDao hrCapacitacionplandetDao;

        public HrCapacitacionplandetServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionplandetDao = servicioProveedor.GetService<HrCapacitacionplandetDao>();
        }

        public HrCapacitacionplandet coreInsertar(UsuarioActual usuarioActual, HrCapacitacionplandet bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return hrCapacitacionplandetDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public HrCapacitacionplandet coreActualizar(UsuarioActual usuarioActual, HrCapacitacionplandet bean)
        {
            return hrCapacitacionplandetDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public HrCapacitacionplandet coreAnular(UsuarioActual usuarioActual, HrCapacitacionplandetPk id)
        {
            return hrCapacitacionplandetDao.coreAnular(usuarioActual,id);
        }

        public HrCapacitacionplandet coreAnular(UsuarioActual usuarioActual, String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion)
        {
            return hrCapacitacionplandetDao.coreAnular(usuarioActual,  pCompanyowner, pAnioplan, pCapacitacion);
        }

        public void coreEliminar(HrCapacitacionplandetPk id)
        {
            hrCapacitacionplandetDao.coreEliminar(id);
        }

        public void coreEliminar(String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion)
        {
            hrCapacitacionplandetDao.coreEliminar( pCompanyowner, pAnioplan, pCapacitacion);
        }


        public HrCapacitacionplandet obtenerPorId(HrCapacitacionplandetPk id)
        {
            return hrCapacitacionplandetDao.obtenerPorId(id);
        }

        public HrCapacitacionplandet obtenerPorId(String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion)
        {
            return hrCapacitacionplandetDao.obtenerPorId( pCompanyowner, pAnioplan, pCapacitacion);
        }

    }
}
