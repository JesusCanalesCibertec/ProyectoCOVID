using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsBeneficiarioIngresoCausalServicio : GenericoServicio {
        PsBeneficiarioIngresoCausal obtenerPorId(PsBeneficiarioIngresoCausalPk id);
        PsBeneficiarioIngresoCausal obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso,String pIdCausal);
        PsBeneficiarioIngresoCausal coreActualizar(UsuarioActual usuarioActual, PsBeneficiarioIngresoCausal bean);
        PsBeneficiarioIngresoCausal coreInsertar(UsuarioActual usuarioActual, PsBeneficiarioIngresoCausal bean);
        void coreEliminar(PsBeneficiarioIngresoCausalPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso,String pIdCausal);
    }
}
