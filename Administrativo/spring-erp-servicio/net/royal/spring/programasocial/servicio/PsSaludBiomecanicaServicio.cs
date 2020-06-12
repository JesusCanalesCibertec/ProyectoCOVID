using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsSaludBiomecanicaServicio : GenericoServicio {
        PsSaludBiomecanica obtenerPorId(PsSaludBiomecanicaPk id);
        PsSaludBiomecanica obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTipoAyuda);
        PsSaludBiomecanica coreActualizar(UsuarioActual usuarioActual, PsSaludBiomecanica bean);
        PsSaludBiomecanica coreInsertar(UsuarioActual usuarioActual, PsSaludBiomecanica bean);
        void coreEliminar(PsSaludBiomecanicaPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTipoAyuda);
    }
}
