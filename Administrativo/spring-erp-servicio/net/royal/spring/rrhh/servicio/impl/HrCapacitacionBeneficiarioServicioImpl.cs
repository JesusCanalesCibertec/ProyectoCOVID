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

public class HrCapacitacionBeneficiarioServicioImpl : GenericoServicioImpl, HrCapacitacionBeneficiarioServicio {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionBeneficiarioDao hrCapacitacionBeneficiarioDao;

        public HrCapacitacionBeneficiarioServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionBeneficiarioDao = servicioProveedor.GetService<HrCapacitacionBeneficiarioDao>();
        }

        public HrCapacitacionBeneficiario coreInsertar(UsuarioActual usuarioActual, HrCapacitacionBeneficiario bean)
        {
            return hrCapacitacionBeneficiarioDao.coreInsertar(usuarioActual,bean);
        }

        public HrCapacitacionBeneficiario coreActualizar(UsuarioActual usuarioActual, HrCapacitacionBeneficiario bean)
        {
            return hrCapacitacionBeneficiarioDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(HrCapacitacionBeneficiarioPk id)
        {
            hrCapacitacionBeneficiarioDao.coreEliminar(id);
        }

        public void coreEliminar(String pCompanyowner,String pCapacitacion,String pIdInstitucion,Nullable<Int32> pIdBeneficiario)
        {
            hrCapacitacionBeneficiarioDao.coreEliminar( pCompanyowner, pCapacitacion, pIdInstitucion, pIdBeneficiario);
        }


        public HrCapacitacionBeneficiario obtenerPorId(HrCapacitacionBeneficiarioPk id)
        {
            return hrCapacitacionBeneficiarioDao.obtenerPorId(id);
        }

        public HrCapacitacionBeneficiario obtenerPorId(String pCompanyowner,String pCapacitacion,String pIdInstitucion,Nullable<Int32> pIdBeneficiario)
        {
            return hrCapacitacionBeneficiarioDao.obtenerPorId( pCompanyowner, pCapacitacion, pIdInstitucion, pIdBeneficiario);
        }

    }
}
