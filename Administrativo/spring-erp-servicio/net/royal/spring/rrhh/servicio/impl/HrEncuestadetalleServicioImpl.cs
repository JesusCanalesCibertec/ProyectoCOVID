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

public class HrEncuestadetalleServicioImpl : GenericoServicioImpl, HrEncuestadetalleServicio {

        private IServiceProvider servicioProveedor;
        private HrEncuestadetalleDao hrEncuestadetalleDao;

        public HrEncuestadetalleServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrEncuestadetalleDao = servicioProveedor.GetService<HrEncuestadetalleDao>();
        }

        public HrEncuestadetalle coreInsertar(UsuarioActual usuarioActual, HrEncuestadetalle bean)
        {
            return hrEncuestadetalleDao.coreInsertar(usuarioActual,bean);
        }

        public HrEncuestadetalle coreActualizar(UsuarioActual usuarioActual, HrEncuestadetalle bean)
        {
            return hrEncuestadetalleDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(HrEncuestadetallePk id)
        {
            hrEncuestadetalleDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pSecuencia,Nullable<Int32> pPregunta)
        {
            hrEncuestadetalleDao.coreEliminar( pSecuencia, pPregunta);
        }


        public HrEncuestadetalle obtenerPorId(HrEncuestadetallePk id)
        {
            return hrEncuestadetalleDao.obtenerPorId(id);
        }

        public HrEncuestadetalle obtenerPorId(Nullable<Int32> pSecuencia,Nullable<Int32> pPregunta)
        {
            return hrEncuestadetalleDao.obtenerPorId( pSecuencia, pPregunta);
        }

    }
}
