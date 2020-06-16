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

public class PsBeneficiarioIngresoDiagnosticoServicioImpl : GenericoServicioImpl, PsBeneficiarioIngresoDiagnosticoServicio {

        private IServiceProvider servicioProveedor;
        private PsBeneficiarioIngresoDiagnosticoDao psBeneficiarioIngresoDiagnosticoDao;

        public PsBeneficiarioIngresoDiagnosticoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psBeneficiarioIngresoDiagnosticoDao = servicioProveedor.GetService<PsBeneficiarioIngresoDiagnosticoDao>();
        }

        public PsBeneficiarioIngresoDiagnostico coreInsertar(UsuarioActual usuarioActual, PsBeneficiarioIngresoDiagnostico bean)
        {
            return psBeneficiarioIngresoDiagnosticoDao.coreInsertar(usuarioActual,bean);
        }

        public PsBeneficiarioIngresoDiagnostico coreActualizar(UsuarioActual usuarioActual, PsBeneficiarioIngresoDiagnostico bean)
        {
            return psBeneficiarioIngresoDiagnosticoDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsBeneficiarioIngresoDiagnosticoPk id)
        {
            psBeneficiarioIngresoDiagnosticoDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso, String pIdDiagnostico)
        {
            psBeneficiarioIngresoDiagnosticoDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdIngreso, pIdDiagnostico);
        }


        public PsBeneficiarioIngresoDiagnostico obtenerPorId(PsBeneficiarioIngresoDiagnosticoPk id)
        {
            return psBeneficiarioIngresoDiagnosticoDao.obtenerPorId(id);
        }

        public PsBeneficiarioIngresoDiagnostico obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso, String pIdDiagnostico)
        {
            return psBeneficiarioIngresoDiagnosticoDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdIngreso, pIdDiagnostico);
        }

    }
}
