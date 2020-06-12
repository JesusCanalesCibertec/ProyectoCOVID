using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsSaludTratamientoDao : GenericoDao<PsSaludTratamiento>
    {
        PsSaludTratamiento obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTratamiento);
        PsSaludTratamiento coreActualizar(UsuarioActual usuarioActual, PsSaludTratamiento bean);
        PsSaludTratamiento coreInsertar(UsuarioActual usuarioActual, PsSaludTratamiento bean);
        void coreEliminar(PsSaludDiscapacidadPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTratamiento);

        List<PsSaludTratamiento> obtenerListado(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
    }
}
