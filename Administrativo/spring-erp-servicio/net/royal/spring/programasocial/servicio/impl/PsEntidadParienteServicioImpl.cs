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

public class PsEntidadParienteServicioImpl : GenericoServicioImpl, PsEntidadParienteServicio {

        private IServiceProvider servicioProveedor;
        private PsEntidadParienteDao psEntidadParienteDao;

        public PsEntidadParienteServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psEntidadParienteDao = servicioProveedor.GetService<PsEntidadParienteDao>();
        }

        public PsEntidadPariente coreInsertar(UsuarioActual usuarioActual, PsEntidadPariente bean)
        {
            return psEntidadParienteDao.coreInsertar(usuarioActual,bean);
        }

        public PsEntidadPariente coreActualizar(UsuarioActual usuarioActual, PsEntidadPariente bean)
        {
            return psEntidadParienteDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsEntidadParientePk id)
        {
            psEntidadParienteDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad,Nullable<Int32> pIdPariente)
        {
            psEntidadParienteDao.coreEliminar( pIdEntidad, pIdPariente);
        }


        public PsEntidadPariente obtenerPorId(PsEntidadParientePk id)
        {
            return psEntidadParienteDao.obtenerPorId(id);
        }

        public PsEntidadPariente obtenerPorId(Nullable<Int32> pIdEntidad,Nullable<Int32> pIdPariente)
        {
            return psEntidadParienteDao.obtenerPorId( pIdEntidad, pIdPariente);
        }

    }
}
