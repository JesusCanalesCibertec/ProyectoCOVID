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

    public class HrEncuestasujetoServicioImpl : GenericoServicioImpl, HrEncuestasujetoServicio
    {

        private IServiceProvider servicioProveedor;
        private HrEncuestasujetoDao hrEncuestasujetoDao;

        public HrEncuestasujetoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            hrEncuestasujetoDao = servicioProveedor.GetService<HrEncuestasujetoDao>();
        }

        public HrEncuestasujeto coreInsertar(UsuarioActual usuarioActual, HrEncuestasujeto bean)
        {
            return hrEncuestasujetoDao.coreInsertar(usuarioActual, bean);
        }

        public HrEncuestasujeto coreActualizar(UsuarioActual usuarioActual, HrEncuestasujeto bean)
        {
            return hrEncuestasujetoDao.coreActualizar(usuarioActual, bean);
        }


        public void coreEliminar(HrEncuestasujetoPk id)
        {
            hrEncuestasujetoDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pSecuencia, Nullable<Int32> pSujeto, Nullable<Int32> pPregunta, String pCompanyowner, String pPeriodo)
        {
            hrEncuestasujetoDao.coreEliminar(pSecuencia, pSujeto, pPregunta, pCompanyowner, pPeriodo);
        }


        public HrEncuestasujeto obtenerPorId(HrEncuestasujetoPk id)
        {
            return hrEncuestasujetoDao.obtenerPorId(id);
        }

    }
}
