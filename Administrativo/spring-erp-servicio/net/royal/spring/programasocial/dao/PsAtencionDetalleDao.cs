using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao {

    public interface PsAtencionDetalleDao : GenericoDao<PsAtencionDetalle> {
        PsAtencionDetalle obtenerPorId(Nullable<Int32> pIdAtencion, Nullable<Int32> pIdDiagnostico);
        PsAtencionDetalle coreActualizar(UsuarioActual usuarioActual, PsAtencionDetalle bean, String estado);
        PsAtencionDetalle coreInsertar(UsuarioActual usuarioActual, PsAtencionDetalle bean, String estado);
        PsAtencionDetalle coreAnular(UsuarioActual usuarioActual, PsAtencionDetallePk id);
        PsAtencionDetalle coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdAtencion, Nullable<Int32> pIdDetalle);
    }
}
