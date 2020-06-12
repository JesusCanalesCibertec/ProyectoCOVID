using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsSaludExamenServicio : GenericoServicio {
        PsSaludExamen obtenerPorId(PsSaludExamenPk id);
        PsSaludExamen obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdExamen,String pIdResultado);
        PsSaludExamen coreActualizar(UsuarioActual usuarioActual, PsSaludExamen bean);
        PsSaludExamen coreInsertar(UsuarioActual usuarioActual, PsSaludExamen bean);
        void coreEliminar(PsSaludExamenPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdExamen,String pIdResultado);
    }
}
