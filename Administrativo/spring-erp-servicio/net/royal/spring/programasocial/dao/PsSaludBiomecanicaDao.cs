using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsSaludBiomecanicaDao : GenericoDao<PsSaludBiomecanica>
    {
        PsSaludBiomecanica obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTipoAyuda);
        PsSaludBiomecanica coreActualizar(UsuarioActual usuarioActual, PsSaludBiomecanica bean);
        PsSaludBiomecanica coreInsertar(UsuarioActual usuarioActual, PsSaludBiomecanica bean);
        void coreEliminar(PsSaludBiomecanicaPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTipoAyuda);
        List<PsSaludBiomecanica> obtenerListado(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
    }
}
