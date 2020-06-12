using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.servicio.impl
{

public class HrCentroestudiosServicioImpl : GenericoServicioImpl, HrCentroestudiosServicio {

        private IServiceProvider servicioProveedor;
        private HrCentroestudiosDao hrCentroestudiosDao;

        public HrCentroestudiosServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrCentroestudiosDao = servicioProveedor.GetService<HrCentroestudiosDao>();
        }

        public HrCentroestudios coreInsertar(UsuarioActual usuarioActual, HrCentroestudios bean)
        {
            return hrCentroestudiosDao.coreInsertar(usuarioActual,bean);
        }

        public HrCentroestudios coreActualizar(UsuarioActual usuarioActual, HrCentroestudios bean)
        {
            return hrCentroestudiosDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(HrCentroestudiosPk id)
        {
            hrCentroestudiosDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pCentro)
        {
            hrCentroestudiosDao.coreEliminar( pCentro);
        }


        public HrCentroestudios obtenerPorId(HrCentroestudiosPk id)
        {
            return hrCentroestudiosDao.obtenerPorId(id);
        }

        public HrCentroestudios obtenerPorId(Nullable<Int32> pCentro)
        {
            return hrCentroestudiosDao.obtenerPorId( pCentro);
        }

        public ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionCentroEstudio filtro)
        {
            return hrCentroestudiosDao.listarBusqueda(paginacion, filtro);
        }

    }
}
