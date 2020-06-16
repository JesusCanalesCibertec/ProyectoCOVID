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

public class PsSaludTratamientoServicioImpl : GenericoServicioImpl, PsSaludTratamientoServicio {

        private IServiceProvider servicioProveedor;
        private PsSaludTratamientoDao PsSaludTratamientoDao;

        public PsSaludTratamientoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            PsSaludTratamientoDao = servicioProveedor.GetService<PsSaludTratamientoDao>();
        }

        public PsSaludTratamiento coreInsertar(UsuarioActual usuarioActual, PsSaludTratamiento bean)
        {
            return PsSaludTratamientoDao.coreInsertar(usuarioActual,bean);
        }

        public PsSaludTratamiento coreActualizar(UsuarioActual usuarioActual, PsSaludTratamiento bean)
        {
            return PsSaludTratamientoDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsSaludDiscapacidadPk id)
        {
            PsSaludTratamientoDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTratamiento)
        {
            PsSaludTratamientoDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTratamiento);
        }


        public PsSaludTratamiento obtenerPorId(PsSaludDiscapacidadPk id)
        {
            return PsSaludTratamientoDao.obtenerPorId(id);
        }

        public PsSaludTratamiento obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTratamiento)
        {
            return PsSaludTratamientoDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTratamiento);
        }

    }
}
