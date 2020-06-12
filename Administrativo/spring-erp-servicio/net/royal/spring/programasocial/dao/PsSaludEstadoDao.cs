using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsSaludEstadoDao : GenericoDao<PsSaludEstado>
    {
        PsSaludEstado obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdEstado);
        PsSaludEstado coreActualizar(UsuarioActual usuarioActual, PsSaludEstado bean);
        PsSaludEstado coreInsertar(UsuarioActual usuarioActual, PsSaludEstado bean);
        void coreEliminar(PsSaludDiscapacidadPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdEstado);

        List<PsSaludEstado> obtenerListado(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud);
    }
}
