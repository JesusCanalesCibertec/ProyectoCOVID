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

public class PsSaludBiomecanicaServicioImpl : GenericoServicioImpl, PsSaludBiomecanicaServicio {

        private IServiceProvider servicioProveedor;
        private PsSaludBiomecanicaDao psSaludBiomecanicaDao;

        public PsSaludBiomecanicaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psSaludBiomecanicaDao = servicioProveedor.GetService<PsSaludBiomecanicaDao>();
        }

        public PsSaludBiomecanica coreInsertar(UsuarioActual usuarioActual, PsSaludBiomecanica bean)
        {
            return psSaludBiomecanicaDao.coreInsertar(usuarioActual,bean);
        }

        public PsSaludBiomecanica coreActualizar(UsuarioActual usuarioActual, PsSaludBiomecanica bean)
        {
            return psSaludBiomecanicaDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsSaludBiomecanicaPk id)
        {
            psSaludBiomecanicaDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTipoAyuda)
        {
            psSaludBiomecanicaDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTipoAyuda);
        }


        public PsSaludBiomecanica obtenerPorId(PsSaludBiomecanicaPk id)
        {
            return psSaludBiomecanicaDao.obtenerPorId(id);
        }

        public PsSaludBiomecanica obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTipoAyuda)
        {
            return psSaludBiomecanicaDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTipoAyuda);
        }

    }
}
