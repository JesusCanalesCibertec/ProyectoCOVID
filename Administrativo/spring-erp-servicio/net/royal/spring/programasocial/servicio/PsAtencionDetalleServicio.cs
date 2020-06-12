using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsAtencionDetalleServicio : GenericoServicio {
        PsAtencionDetalle obtenerPorId(PsAtencionDetallePk id);
        PsAtencionDetalle obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdAtencion,Nullable<Int32> pIdDetalle);
        PsAtencionDetalle coreActualizar(UsuarioActual usuarioActual, PsAtencionDetalle bean);
        PsAtencionDetalle coreInsertar(UsuarioActual usuarioActual, PsAtencionDetalle bean);
        PsAtencionDetalle coreAnular(UsuarioActual usuarioActual,PsAtencionDetallePk id);
        PsAtencionDetalle coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdAtencion,Nullable<Int32> pIdDetalle);
    }
}
