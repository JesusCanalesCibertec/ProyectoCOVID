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

public class PsAtencionDetalleServicioImpl : GenericoServicioImpl, PsAtencionDetalleServicio {

        private IServiceProvider servicioProveedor;
        private PsAtencionDetalleDao psAtencionDetalleDao;

        public PsAtencionDetalleServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psAtencionDetalleDao = servicioProveedor.GetService<PsAtencionDetalleDao>();
        }

        public PsAtencionDetalle coreInsertar(UsuarioActual usuarioActual, PsAtencionDetalle bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return psAtencionDetalleDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public PsAtencionDetalle coreActualizar(UsuarioActual usuarioActual, PsAtencionDetalle bean)
        {
            return psAtencionDetalleDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public PsAtencionDetalle coreAnular(UsuarioActual usuarioActual, PsAtencionDetallePk id)
        {
            return psAtencionDetalleDao.coreAnular(usuarioActual,id);
        }

        public PsAtencionDetalle coreAnular(UsuarioActual usuarioActual, String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdAtencion,Nullable<Int32> pIdDetalle)
        {
            return psAtencionDetalleDao.coreAnular(usuarioActual,  pIdInstitucion, pIdBeneficiario, pIdAtencion, pIdDetalle);
        }


        public PsAtencionDetalle obtenerPorId(PsAtencionDetallePk id)
        {
            return psAtencionDetalleDao.obtenerPorId(id);
        }

        public PsAtencionDetalle obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdAtencion,Nullable<Int32> pIdDetalle)
        {
            return psAtencionDetalleDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdAtencion, pIdDetalle);
        }

    }
}
