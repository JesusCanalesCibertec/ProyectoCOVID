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
using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.dominio;

namespace net.royal.spring.rrhh.servicio.impl
{

public class PmPlantillaTareasServicioImpl : GenericoServicioImpl, PmPlantillaTareasServicio {

        private IServiceProvider servicioProveedor;
        private PmPlantillaTareasDao hrEncuestaplantilladetalleDao;

        public PmPlantillaTareasServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrEncuestaplantilladetalleDao = servicioProveedor.GetService<PmPlantillaTareasDao>();
        }

        public PmPlantillaTareas coreInsertar(UsuarioActual usuarioActual, PmPlantillaTareas bean)
        {
            return hrEncuestaplantilladetalleDao.coreInsertar(usuarioActual,bean);
        }

        public PmPlantillaTareas coreActualizar(UsuarioActual usuarioActual, PmPlantillaTareas bean)
        {
            return hrEncuestaplantilladetalleDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PmPlantillaTareasPk id)
        {
            hrEncuestaplantilladetalleDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pPlantilla,Nullable<Int32> pPregunta)
        {
            hrEncuestaplantilladetalleDao.coreEliminar( pPlantilla, pPregunta);
        }


        public PmPlantillaTareas obtenerPorId(PmPlantillaTareasPk id)
        {
            return hrEncuestaplantilladetalleDao.obtenerPorId(id);
        }

        public PmPlantillaTareas obtenerPorId(Nullable<Int32> pPlantilla,Nullable<Int32> pPregunta)
        {
            return hrEncuestaplantilladetalleDao.obtenerPorId( pPlantilla, pPregunta);
        }

    }
}
