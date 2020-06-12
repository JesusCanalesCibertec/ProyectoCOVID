using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsSaludExamenDao : GenericoDao<PsSaludExamen>
    {
        PsSaludExamen obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdExamen,String pIdResultado);
        PsSaludExamen coreActualizar(UsuarioActual usuarioActual, PsSaludExamen bean);
        PsSaludExamen coreInsertar(UsuarioActual usuarioActual, PsSaludExamen bean);
        void coreEliminar(PsSaludExamenPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdExamen,String pIdResultado);
        List<PsSaludExamen> obtenerListado(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
    }
}
