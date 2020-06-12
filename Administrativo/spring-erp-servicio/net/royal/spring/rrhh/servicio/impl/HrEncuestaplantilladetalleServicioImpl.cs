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

namespace net.royal.spring.rrhh.servicio.impl
{

public class HrEncuestaplantilladetalleServicioImpl : GenericoServicioImpl, HrEncuestaplantilladetalleServicio {

        private IServiceProvider servicioProveedor;
        private HrEncuestaplantilladetalleDao hrEncuestaplantilladetalleDao;

        public HrEncuestaplantilladetalleServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            hrEncuestaplantilladetalleDao = servicioProveedor.GetService<HrEncuestaplantilladetalleDao>();
        }

        public HrEncuestaplantilladetalle coreInsertar(UsuarioActual usuarioActual, HrEncuestaplantilladetalle bean)
        {
            return hrEncuestaplantilladetalleDao.coreInsertar(usuarioActual, bean);
        }

        public HrEncuestaplantilladetalle coreActualizar(UsuarioActual usuarioActual, HrEncuestaplantilladetalle bean)
        {
            return hrEncuestaplantilladetalleDao.coreActualizar(usuarioActual, bean);
        }


        public void coreEliminar(HrEncuestaplantilladetallePk id)
        {
            hrEncuestaplantilladetalleDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pPlantilla, Nullable<Int32> pPregunta)
        {
            hrEncuestaplantilladetalleDao.coreEliminar(pPlantilla, pPregunta);
        }


        public HrEncuestaplantilladetalle obtenerPorId(HrEncuestaplantilladetallePk id)
        {
            return hrEncuestaplantilladetalleDao.obtenerPorId(id);
        }

        public HrEncuestaplantilladetalle obtenerPorId(Nullable<Int32> pPlantilla, Nullable<Int32> pPregunta)
        {
            return hrEncuestaplantilladetalleDao.obtenerPorId(pPlantilla, pPregunta);
        }

    }
}
