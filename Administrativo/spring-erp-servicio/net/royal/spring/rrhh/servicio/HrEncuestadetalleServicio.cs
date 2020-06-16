using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrEncuestadetalleServicio : GenericoServicio {
        HrEncuestadetalle obtenerPorId(HrEncuestadetallePk id);
        HrEncuestadetalle obtenerPorId(Nullable<Int32> pSecuencia,Nullable<Int32> pPregunta);
        HrEncuestadetalle coreActualizar(UsuarioActual usuarioActual, HrEncuestadetalle bean);
        HrEncuestadetalle coreInsertar(UsuarioActual usuarioActual, HrEncuestadetalle bean);
        void coreEliminar(HrEncuestadetallePk id);
        void coreEliminar(Nullable<Int32> pSecuencia,Nullable<Int32> pPregunta);
    }
}
