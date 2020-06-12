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

public class PsEntidadProgramaSocialServicioImpl : GenericoServicioImpl, PsEntidadProgramaSocialServicio {

        private IServiceProvider servicioProveedor;
        private PsEntidadProgramaSocialDao psEntidadProgramaSocialDao;

        public PsEntidadProgramaSocialServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psEntidadProgramaSocialDao = servicioProveedor.GetService<PsEntidadProgramaSocialDao>();
        }

        public PsEntidadProgramaSocial coreInsertar(UsuarioActual usuarioActual, PsEntidadProgramaSocial bean)
        {
            return psEntidadProgramaSocialDao.coreInsertar(usuarioActual,bean);
        }

        public PsEntidadProgramaSocial coreActualizar(UsuarioActual usuarioActual, PsEntidadProgramaSocial bean)
        {
            return psEntidadProgramaSocialDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsEntidadProgramaSocialPk id)
        {
            psEntidadProgramaSocialDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad,String pIdProgramaSocial)
        {
            psEntidadProgramaSocialDao.coreEliminar( pIdEntidad, pIdProgramaSocial);
        }


        public PsEntidadProgramaSocial obtenerPorId(PsEntidadProgramaSocialPk id)
        {
            return psEntidadProgramaSocialDao.obtenerPorId(id);
        }

        public PsEntidadProgramaSocial obtenerPorId(Nullable<Int32> pIdEntidad,String pIdProgramaSocial)
        {
            return psEntidadProgramaSocialDao.obtenerPorId( pIdEntidad, pIdProgramaSocial);
        }

    }
}
