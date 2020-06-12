using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsCapacidadTallerServicio : GenericoServicio {
        PsCapacidadTaller obtenerPorId(PsCapacidadTallerPk id);
        PsCapacidadTaller obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdCapacidad, Nullable<Int32> pIdTaller);
        PsCapacidadTaller coreActualizar(UsuarioActual usuarioActual, PsCapacidadTaller bean);
        PsCapacidadTaller coreInsertar(UsuarioActual usuarioActual, PsCapacidadTaller bean);
        void coreEliminar(PsCapacidadTallerPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdCapacidad, Nullable<Int32> pIdTaller);
    }
}
