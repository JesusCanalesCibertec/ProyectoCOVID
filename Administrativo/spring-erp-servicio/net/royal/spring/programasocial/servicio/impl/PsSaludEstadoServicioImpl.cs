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

public class PsSaludEstadoServicioImpl : GenericoServicioImpl, PsSaludEstadoServicio {

        private IServiceProvider servicioProveedor;
        private PsSaludEstadoDao psSaludEstadoDao;

        public PsSaludEstadoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psSaludEstadoDao = servicioProveedor.GetService<PsSaludEstadoDao>();
        }

        public PsSaludEstado coreInsertar(UsuarioActual usuarioActual, PsSaludEstado bean)
        {
            return psSaludEstadoDao.coreInsertar(usuarioActual,bean);
        }

        public PsSaludEstado coreActualizar(UsuarioActual usuarioActual, PsSaludEstado bean)
        {
            return psSaludEstadoDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsSaludDiscapacidadPk id)
        {
            psSaludEstadoDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdEstado)
        {
            psSaludEstadoDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdEstado);
        }


        public PsSaludEstado obtenerPorId(PsSaludDiscapacidadPk id)
        {
            return psSaludEstadoDao.obtenerPorId(id);
        }

        public PsSaludEstado obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdEstado)
        {
            return psSaludEstadoDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdEstado);
        }

    }
}
