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

public class PsEntidadEquipamientoServicioImpl : GenericoServicioImpl, PsEntidadEquipamientoServicio {

        private IServiceProvider servicioProveedor;
        private PsEntidadEquipamientoDao psEntidadEquipamientoDao;

        public PsEntidadEquipamientoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psEntidadEquipamientoDao = servicioProveedor.GetService<PsEntidadEquipamientoDao>();
        }

        public PsEntidadEquipamiento coreInsertar(UsuarioActual usuarioActual, PsEntidadEquipamiento bean)
        {
            return psEntidadEquipamientoDao.coreInsertar(usuarioActual,bean);
        }

        public PsEntidadEquipamiento coreActualizar(UsuarioActual usuarioActual, PsEntidadEquipamiento bean)
        {
            return psEntidadEquipamientoDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsEntidadEquipamientoPk id)
        {
            psEntidadEquipamientoDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad,String pIdEquipamiento)
        {
            psEntidadEquipamientoDao.coreEliminar( pIdEntidad, pIdEquipamiento);
        }


        public PsEntidadEquipamiento obtenerPorId(PsEntidadEquipamientoPk id)
        {
            return psEntidadEquipamientoDao.obtenerPorId(id);
        }

        public PsEntidadEquipamiento obtenerPorId(Nullable<Int32> pIdEntidad,String pIdEquipamiento)
        {
            return psEntidadEquipamientoDao.obtenerPorId( pIdEntidad, pIdEquipamiento);
        }

    }
}
