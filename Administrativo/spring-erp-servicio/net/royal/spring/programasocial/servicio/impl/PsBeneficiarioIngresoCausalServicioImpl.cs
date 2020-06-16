using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.programasocial.servicio.impl
{

public class PsBeneficiarioIngresoCausalServicioImpl : GenericoServicioImpl, PsBeneficiarioIngresoCausalServicio {

        private IServiceProvider servicioProveedor;
        private PsBeneficiarioIngresoCausalDao psBeneficiarioIngresoCausalDao;

        public PsBeneficiarioIngresoCausalServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psBeneficiarioIngresoCausalDao = servicioProveedor.GetService<PsBeneficiarioIngresoCausalDao>();
        }

        public PsBeneficiarioIngresoCausal coreInsertar(UsuarioActual usuarioActual, PsBeneficiarioIngresoCausal bean)
        {
            return psBeneficiarioIngresoCausalDao.coreInsertar(usuarioActual,bean);
        }

        public PsBeneficiarioIngresoCausal coreActualizar(UsuarioActual usuarioActual, PsBeneficiarioIngresoCausal bean)
        {
            return psBeneficiarioIngresoCausalDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsBeneficiarioIngresoCausalPk id)
        {
            psBeneficiarioIngresoCausalDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso,String pIdCausal)
        {
            psBeneficiarioIngresoCausalDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdIngreso, pIdCausal);
        }


        public PsBeneficiarioIngresoCausal obtenerPorId(PsBeneficiarioIngresoCausalPk id)
        {
            return psBeneficiarioIngresoCausalDao.obtenerPorId(id);
        }

        public PsBeneficiarioIngresoCausal obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso,String pIdCausal)
        {
            return psBeneficiarioIngresoCausalDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdIngreso, pIdCausal);
        }

    }
}
