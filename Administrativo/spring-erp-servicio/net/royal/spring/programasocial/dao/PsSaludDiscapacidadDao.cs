using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsSaludDiscapacidadDao : GenericoDao<PsSaludDiscapacidad>
    {
        PsSaludDiscapacidad obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiscapacidad);
        PsSaludDiscapacidad coreActualizar(UsuarioActual usuarioActual, PsSaludDiscapacidad bean);
        PsSaludDiscapacidad coreInsertar(UsuarioActual usuarioActual, PsSaludDiscapacidad bean);
        void coreEliminar(PsSaludDiscapacidadPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiscapacidad);

        List<PsSaludDiscapacidad> obtenerListado(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
    }
}
