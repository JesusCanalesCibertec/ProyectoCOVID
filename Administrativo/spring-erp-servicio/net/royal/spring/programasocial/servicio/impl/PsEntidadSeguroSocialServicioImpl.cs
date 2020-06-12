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

public class PsEntidadSeguroSocialServicioImpl : GenericoServicioImpl, PsEntidadSeguroSocialServicio {

        private IServiceProvider servicioProveedor;
        private PsEntidadSeguroSocialDao psEntidadSeguroSocialDao;

        public PsEntidadSeguroSocialServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psEntidadSeguroSocialDao = servicioProveedor.GetService<PsEntidadSeguroSocialDao>();
        }

        public PsEntidadSeguroSocial coreInsertar(UsuarioActual usuarioActual, PsEntidadSeguroSocial bean)
        {
            return psEntidadSeguroSocialDao.coreInsertar(usuarioActual,bean);
        }

        public PsEntidadSeguroSocial coreActualizar(UsuarioActual usuarioActual, PsEntidadSeguroSocial bean)
        {
            return psEntidadSeguroSocialDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsEntidadSeguroSocialPk id)
        {
            psEntidadSeguroSocialDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad,String pIdSeguroSocial)
        {
            psEntidadSeguroSocialDao.coreEliminar( pIdEntidad, pIdSeguroSocial);
        }


        public PsEntidadSeguroSocial obtenerPorId(PsEntidadSeguroSocialPk id)
        {
            return psEntidadSeguroSocialDao.obtenerPorId(id);
        }

        public PsEntidadSeguroSocial obtenerPorId(Nullable<Int32> pIdEntidad,String pIdSeguroSocial)
        {
            return psEntidadSeguroSocialDao.obtenerPorId( pIdEntidad, pIdSeguroSocial);
        }

    }
}
