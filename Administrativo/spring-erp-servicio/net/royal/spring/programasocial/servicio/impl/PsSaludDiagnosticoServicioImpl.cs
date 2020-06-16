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

public class PsSaludDiagnosticoServicioImpl : GenericoServicioImpl, PsSaludDiagnosticoServicio {

        private IServiceProvider servicioProveedor;
        private PsSaludDiagnosticoDao PsSaludDiagnosticoDao;

        public PsSaludDiagnosticoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            PsSaludDiagnosticoDao = servicioProveedor.GetService<PsSaludDiagnosticoDao>();
        }

        public PsSaludDiagnostico coreInsertar(UsuarioActual usuarioActual, PsSaludDiagnostico bean)
        {
            return PsSaludDiagnosticoDao.coreInsertar(usuarioActual,bean);
        }

        public PsSaludDiagnostico coreActualizar(UsuarioActual usuarioActual, PsSaludDiagnostico bean)
        {
            return PsSaludDiagnosticoDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsSaludDiscapacidadPk id)
        {
            PsSaludDiagnosticoDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiagnostico)
        {
            PsSaludDiagnosticoDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdDiagnostico);
        }


        public PsSaludDiagnostico obtenerPorId(PsSaludDiscapacidadPk id)
        {
            return PsSaludDiagnosticoDao.obtenerPorId(id);
        }

        public PsSaludDiagnostico obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiagnostico)
        {
            return PsSaludDiagnosticoDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdDiagnostico);
        }

    }
}
