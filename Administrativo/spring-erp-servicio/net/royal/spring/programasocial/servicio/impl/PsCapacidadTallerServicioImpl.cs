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

public class PsCapacidadTallerServicioImpl : GenericoServicioImpl, PsCapacidadTallerServicio {

        private IServiceProvider servicioProveedor;
        private PsCapacidadTallerDao psCapacidadTallerDao;

        public PsCapacidadTallerServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psCapacidadTallerDao = servicioProveedor.GetService<PsCapacidadTallerDao>();
        }

        public PsCapacidadTaller coreInsertar(UsuarioActual usuarioActual, PsCapacidadTaller bean)
        {
            return psCapacidadTallerDao.coreInsertar(usuarioActual,bean);
        }

        public PsCapacidadTaller coreActualizar(UsuarioActual usuarioActual, PsCapacidadTaller bean)
        {
            return psCapacidadTallerDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsCapacidadTallerPk id)
        {
            psCapacidadTallerDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdCapacidad, Nullable<Int32> pIdTaller)
        {
            psCapacidadTallerDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdCapacidad, pIdTaller);
        }


        public PsCapacidadTaller obtenerPorId(PsCapacidadTallerPk id)
        {
            return psCapacidadTallerDao.obtenerPorId(id);
        }

        public PsCapacidadTaller obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdCapacidad, Nullable<Int32> pIdTaller)
        {
            return psCapacidadTallerDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdCapacidad, pIdTaller);
        }

       
    }
}
