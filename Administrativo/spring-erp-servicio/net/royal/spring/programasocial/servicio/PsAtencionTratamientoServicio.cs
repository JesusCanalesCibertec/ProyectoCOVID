using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsAtencionTratamientoServicio : GenericoServicio {
        PsAtencionTratamiento obtenerPorId(PsAtencionTratamientoPk id);
        PsAtencionTratamiento obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,
            Nullable<Int32> pIdAtencion,Nullable<Int32> pIdDetalle, String pIdTratamiento);
        PsAtencionTratamiento coreActualizar(UsuarioActual usuarioActual, PsAtencionTratamiento bean);
        PsAtencionTratamiento coreInsertar(UsuarioActual usuarioActual, PsAtencionTratamiento bean);
    }
}
