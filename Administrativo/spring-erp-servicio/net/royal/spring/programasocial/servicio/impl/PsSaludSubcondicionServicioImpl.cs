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

public class PsSaludSubcondicionServicioImpl : GenericoServicioImpl, PsSaludSubcondicionServicio {

        private IServiceProvider servicioProveedor;
        private PsSaludSubcondicionDao psSaludSubcondicionDao;

        public PsSaludSubcondicionServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psSaludSubcondicionDao = servicioProveedor.GetService<PsSaludSubcondicionDao>();
        }

        public PsSaludSubcondicion coreInsertar(UsuarioActual usuarioActual, PsSaludSubcondicion bean)
        {
            return psSaludSubcondicionDao.coreInsertar(usuarioActual,bean);
        }

        public PsSaludSubcondicion coreActualizar(UsuarioActual usuarioActual, PsSaludSubcondicion bean)
        {
            return psSaludSubcondicionDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsSaludSubcondicionPk id)
        {
            psSaludSubcondicionDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdCondicion,String pIdSubcondicion)
        {
            psSaludSubcondicionDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdCondicion, pIdSubcondicion);
        }


        public PsSaludSubcondicion obtenerPorId(PsSaludSubcondicionPk id)
        {
            return psSaludSubcondicionDao.obtenerPorId(id);
        }

        public PsSaludSubcondicion obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdCondicion,String pIdSubcondicion)
        {
            return psSaludSubcondicionDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdCondicion, pIdSubcondicion);
        }

    }
}
