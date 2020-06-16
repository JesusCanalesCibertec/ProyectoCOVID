using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsAtencionTratamientoDao : GenericoDao<PsAtencionTratamiento>
    {
        PsAtencionTratamiento obtenerPorId(Nullable<Int32> pIdAtencion, Nullable<Int32> pIdDiagnostico, String pIdTratamiento);
        PsAtencionTratamiento coreActualizar(UsuarioActual usuarioActual, PsAtencionTratamiento bean, String estado);
        PsAtencionTratamiento coreInsertar(UsuarioActual usuarioActual, PsAtencionTratamiento bean, String estado);
        void coreEliminar(PsAtencionTratamientoPk id);
        void coreEliminar(Nullable<Int32> pIdAtencion, Nullable<Int32> pIdDiagnostico, String pIdTratamiento);
        PsAtencionTratamiento coreAnular(UsuarioActual usuarioActual,PsAtencionTratamientoPk id);
        PsAtencionTratamiento coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdAtencion, Nullable<Int32> pIdDiagnostico, String pIdTratamiento);
    }
}
