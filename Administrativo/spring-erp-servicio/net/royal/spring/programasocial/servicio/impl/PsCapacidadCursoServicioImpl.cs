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

public class PsCapacidadCursoServicioImpl : GenericoServicioImpl, PsCapacidadCursoServicio {

        private IServiceProvider servicioProveedor;
        private PsCapacidadCursoDao psCapacidadCursoDao;

        public PsCapacidadCursoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psCapacidadCursoDao = servicioProveedor.GetService<PsCapacidadCursoDao>();
        }

        public PsCapacidadCurso coreInsertar(UsuarioActual usuarioActual, PsCapacidadCurso bean)
        {
            return psCapacidadCursoDao.coreInsertar(usuarioActual,bean);
        }

        public PsCapacidadCurso coreActualizar(UsuarioActual usuarioActual, PsCapacidadCurso bean)
        {
            return psCapacidadCursoDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsCapacidadCursoPk id)
        {
            psCapacidadCursoDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdCapacidad,String pIdCurso)
        {
            psCapacidadCursoDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdCapacidad, pIdCurso);
        }


        public PsCapacidadCurso obtenerPorId(PsCapacidadCursoPk id)
        {
            return psCapacidadCursoDao.obtenerPorId(id);
        }

        public PsCapacidadCurso obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdCapacidad,String pIdCurso)
        {
            return psCapacidadCursoDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdCapacidad, pIdCurso);
        }

    }
}
