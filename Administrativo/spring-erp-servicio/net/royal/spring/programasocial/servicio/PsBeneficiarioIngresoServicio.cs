using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsBeneficiarioIngresoServicio : GenericoServicio {
        PsBeneficiarioIngreso obtenerUltimoIngreso(PsBeneficiarioIngresoPk id);
        PsBeneficiarioIngreso obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso);
        PsBeneficiarioIngreso coreActualizar(UsuarioActual usuarioActual, PsBeneficiarioIngreso bean);
        PsBeneficiarioIngreso coreInsertar(UsuarioActual usuarioActual, PsBeneficiarioIngreso bean);
        void coreEliminar(PsBeneficiarioIngresoPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso);
        PsBeneficiarioIngreso coreAnular(UsuarioActual usuarioActual,PsBeneficiarioIngresoPk id);
        PsBeneficiarioIngreso coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso);
        List<DtoBeneficiarioIngreso> listado(Int32 pIdBeneficiario, string pIdInstitucion);
    }
}
