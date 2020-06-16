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

public class PsAtencionTratamientoServicioImpl : GenericoServicioImpl, PsAtencionTratamientoServicio {

        private IServiceProvider servicioProveedor;
        private PsAtencionTratamientoDao psAtencionTratamientoDao;

        public PsAtencionTratamientoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psAtencionTratamientoDao = servicioProveedor.GetService<PsAtencionTratamientoDao>();
        }

        public PsAtencionTratamiento coreInsertar(UsuarioActual usuarioActual, PsAtencionTratamiento bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return psAtencionTratamientoDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public PsAtencionTratamiento coreActualizar(UsuarioActual usuarioActual, PsAtencionTratamiento bean)
        {
            return psAtencionTratamientoDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public void coreEliminar(PsAtencionTratamientoPk id)
        {
            psAtencionTratamientoDao.coreEliminar(id);
        }

        public PsAtencionTratamiento obtenerPorId(PsAtencionTratamientoPk id)
        {
            return psAtencionTratamientoDao.obtenerPorId(id);
        }

        public PsAtencionTratamiento obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,
            Nullable<Int32> pIdAtencion,Nullable<Int32> pIdDetalle, String pIdTratamiento)
        {
            return psAtencionTratamientoDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdAtencion, pIdDetalle, pIdTratamiento);
        }

    }
}
