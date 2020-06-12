using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsSaludAyudaDao : GenericoDao<PsSaludAyuda>
    {
        PsSaludAyuda obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdEstado);
        PsSaludAyuda coreActualizar(UsuarioActual usuarioActual, PsSaludAyuda bean);
        PsSaludAyuda coreInsertar(UsuarioActual usuarioActual, PsSaludAyuda bean);
        void coreEliminar(PsSaludAyudaPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdEstado);

        List<PsSaludAyuda> obtenerListado(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
    }
}
