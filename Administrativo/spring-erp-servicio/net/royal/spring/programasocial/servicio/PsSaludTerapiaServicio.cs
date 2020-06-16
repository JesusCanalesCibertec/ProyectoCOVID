using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsSaludTerapiaServicio : GenericoServicio {
        PsSaludTerapia obtenerPorId(PsSaludTerapiaPk id);
        PsSaludTerapia obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTerapia);
        PsSaludTerapia coreActualizar(UsuarioActual usuarioActual, PsSaludTerapia bean);
        PsSaludTerapia coreInsertar(UsuarioActual usuarioActual, PsSaludTerapia bean);
        void coreEliminar(PsSaludTerapiaPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTerapia);
    }
}
