using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsSaludSubcondicionServicio : GenericoServicio {
        PsSaludSubcondicion obtenerPorId(PsSaludSubcondicionPk id);
        PsSaludSubcondicion obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdCondicion,String pIdSubcondicion);
        PsSaludSubcondicion coreActualizar(UsuarioActual usuarioActual, PsSaludSubcondicion bean);
        PsSaludSubcondicion coreInsertar(UsuarioActual usuarioActual, PsSaludSubcondicion bean);
        void coreEliminar(PsSaludSubcondicionPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdCondicion,String pIdSubcondicion);
    }
}
