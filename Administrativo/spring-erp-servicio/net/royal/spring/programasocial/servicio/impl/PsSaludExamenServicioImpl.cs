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

public class PsSaludExamenServicioImpl : GenericoServicioImpl, PsSaludExamenServicio {

        private IServiceProvider servicioProveedor;
        private PsSaludExamenDao psSaludExamenDao;

        public PsSaludExamenServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psSaludExamenDao = servicioProveedor.GetService<PsSaludExamenDao>();
        }

        public PsSaludExamen coreInsertar(UsuarioActual usuarioActual, PsSaludExamen bean)
        {
            return psSaludExamenDao.coreInsertar(usuarioActual,bean);
        }

        public PsSaludExamen coreActualizar(UsuarioActual usuarioActual, PsSaludExamen bean)
        {
            return psSaludExamenDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsSaludExamenPk id)
        {
            psSaludExamenDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdExamen,String pIdResultado)
        {
            psSaludExamenDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdExamen, pIdResultado);
        }


        public PsSaludExamen obtenerPorId(PsSaludExamenPk id)
        {
            return psSaludExamenDao.obtenerPorId(id);
        }

        public PsSaludExamen obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdExamen,String pIdResultado)
        {
            return psSaludExamenDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdExamen, pIdResultado);
        }

    }
}
