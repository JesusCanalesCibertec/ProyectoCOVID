using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsSaludEstadoServicio : GenericoServicio {
        PsSaludEstado obtenerPorId(PsSaludDiscapacidadPk id);
        PsSaludEstado obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdEstado);
        PsSaludEstado coreActualizar(UsuarioActual usuarioActual, PsSaludEstado bean);
        PsSaludEstado coreInsertar(UsuarioActual usuarioActual, PsSaludEstado bean);
        void coreEliminar(PsSaludDiscapacidadPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdEstado);
    }
}
