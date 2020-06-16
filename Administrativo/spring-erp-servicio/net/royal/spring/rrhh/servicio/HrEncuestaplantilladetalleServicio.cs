using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrEncuestaplantilladetalleServicio : GenericoServicio {
        HrEncuestaplantilladetalle obtenerPorId(HrEncuestaplantilladetallePk id);
        HrEncuestaplantilladetalle obtenerPorId(Nullable<Int32> pPlantilla,Nullable<Int32> pPregunta);
        HrEncuestaplantilladetalle coreActualizar(UsuarioActual usuarioActual, HrEncuestaplantilladetalle bean);
        HrEncuestaplantilladetalle coreInsertar(UsuarioActual usuarioActual, HrEncuestaplantilladetalle bean);
        void coreEliminar(HrEncuestaplantilladetallePk id);
        void coreEliminar(Nullable<Int32> pPlantilla,Nullable<Int32> pPregunta);
    }
}
