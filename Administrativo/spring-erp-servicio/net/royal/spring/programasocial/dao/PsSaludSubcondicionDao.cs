using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsSaludSubcondicionDao : GenericoDao<PsSaludSubcondicion>
    {
        PsSaludSubcondicion obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdCondicion,String pIdSubcondicion);
        PsSaludSubcondicion coreActualizar(UsuarioActual usuarioActual, PsSaludSubcondicion bean);
        PsSaludSubcondicion coreInsertar(UsuarioActual usuarioActual, PsSaludSubcondicion bean);
        void coreEliminar(PsSaludSubcondicionPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdCondicion,String pIdSubcondicion);
        List<PsSaludSubcondicion> obtenerListado(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
    }
}
