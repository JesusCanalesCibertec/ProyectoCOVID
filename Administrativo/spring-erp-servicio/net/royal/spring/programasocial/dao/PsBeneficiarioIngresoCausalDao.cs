using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsBeneficiarioIngresoCausalDao : GenericoDao<PsBeneficiarioIngresoCausal>
    {
        PsBeneficiarioIngresoCausal obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso,String pIdCausal);
        PsBeneficiarioIngresoCausal coreActualizar(UsuarioActual usuarioActual, PsBeneficiarioIngresoCausal bean);
        PsBeneficiarioIngresoCausal coreInsertar(UsuarioActual usuarioActual, PsBeneficiarioIngresoCausal bean);
        void coreEliminar(PsBeneficiarioIngresoCausalPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso,String pIdCausal);
        List<PsBeneficiarioIngresoCausal> listarCausal(string institucion, int idBeneficiario, int idUltimoIngreso);
    }
}
