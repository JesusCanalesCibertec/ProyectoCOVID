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

public class HrCapacitacionplanServicioImpl : GenericoServicioImpl, HrCapacitacionplanServicio {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionplanDao hrCapacitacionplanDao;

        public HrCapacitacionplanServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionplanDao = servicioProveedor.GetService<HrCapacitacionplanDao>();
        }

        public HrCapacitacionplan coreInsertar(UsuarioActual usuarioActual, HrCapacitacionplan bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return hrCapacitacionplanDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public HrCapacitacionplan coreActualizar(UsuarioActual usuarioActual, HrCapacitacionplan bean)
        {
            return hrCapacitacionplanDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public HrCapacitacionplan coreAnular(UsuarioActual usuarioActual, HrCapacitacionplanPk id)
        {
            return hrCapacitacionplanDao.coreAnular(usuarioActual,id);
        }

        public HrCapacitacionplan coreAnular(UsuarioActual usuarioActual, String pCompanyowner,Nullable<Decimal> pAnioplan)
        {
            return hrCapacitacionplanDao.coreAnular(usuarioActual,  pCompanyowner, pAnioplan);
        }

        public void coreEliminar(HrCapacitacionplanPk id)
        {
            hrCapacitacionplanDao.coreEliminar(id);
        }

        public void coreEliminar(String pCompanyowner,Nullable<Decimal> pAnioplan)
        {
            hrCapacitacionplanDao.coreEliminar( pCompanyowner, pAnioplan);
        }


        public HrCapacitacionplan obtenerPorId(HrCapacitacionplanPk id)
        {
            return hrCapacitacionplanDao.obtenerPorId(id);
        }

        public HrCapacitacionplan obtenerPorId(String pCompanyowner,Nullable<Decimal> pAnioplan)
        {
            return hrCapacitacionplanDao.obtenerPorId( pCompanyowner, pAnioplan);
        }

    }
}
