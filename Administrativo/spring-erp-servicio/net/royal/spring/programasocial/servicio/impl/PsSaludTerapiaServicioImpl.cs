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

public class PsSaludTerapiaServicioImpl : GenericoServicioImpl, PsSaludTerapiaServicio {

        private IServiceProvider servicioProveedor;
        private PsSaludTerapiaDao psSaludTerapiaDao;

        public PsSaludTerapiaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psSaludTerapiaDao = servicioProveedor.GetService<PsSaludTerapiaDao>();
        }

        public PsSaludTerapia coreInsertar(UsuarioActual usuarioActual, PsSaludTerapia bean)
        {
            return psSaludTerapiaDao.coreInsertar(usuarioActual,bean);
        }

        public PsSaludTerapia coreActualizar(UsuarioActual usuarioActual, PsSaludTerapia bean)
        {
            return psSaludTerapiaDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsSaludTerapiaPk id)
        {
            psSaludTerapiaDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTerapia)
        {
            psSaludTerapiaDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTerapia);
        }


        public PsSaludTerapia obtenerPorId(PsSaludTerapiaPk id)
        {
            return psSaludTerapiaDao.obtenerPorId(id);
        }

        public PsSaludTerapia obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTerapia)
        {
            return psSaludTerapiaDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTerapia);
        }

    }
}
