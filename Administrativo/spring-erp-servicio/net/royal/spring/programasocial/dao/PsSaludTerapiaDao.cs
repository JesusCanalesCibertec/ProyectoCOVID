using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsSaludTerapiaDao : GenericoDao<PsSaludTerapia>
    {
        PsSaludTerapia obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTerapia);
        PsSaludTerapia coreActualizar(UsuarioActual usuarioActual, PsSaludTerapia bean);
        PsSaludTerapia coreInsertar(UsuarioActual usuarioActual, PsSaludTerapia bean);
        void coreEliminar(PsSaludTerapiaPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTerapia);
        List<PsSaludTerapia> obtenerListado(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
    }
}
