using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao
{

    public interface PsBeneficiarioIngresoDao : GenericoDao<PsBeneficiarioIngreso>
    {
        PsBeneficiarioIngreso obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso);
        PsBeneficiarioIngreso coreActualizar(UsuarioActual usuarioActual, PsBeneficiarioIngreso bean, String estado);
        PsBeneficiarioIngreso coreInsertar(UsuarioActual usuarioActual, PsBeneficiarioIngreso bean, String estado);
        void coreEliminar(PsBeneficiarioIngresoPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso);
        PsBeneficiarioIngreso coreAnular(UsuarioActual usuarioActual,PsBeneficiarioIngresoPk id);
        PsBeneficiarioIngreso coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso);

        int generarCodigo(Int32? pIdBeneficiario);

        int ObtenerUltimoIngreso(Int32? pIdBeneficiario);
        List<DtoBeneficiarioIngreso> listado(int pIdBeneficiario, string pIdInstitucion);
    }
}
