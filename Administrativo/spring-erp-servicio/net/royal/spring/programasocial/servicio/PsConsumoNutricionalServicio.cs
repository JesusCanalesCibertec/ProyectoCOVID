using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsConsumoNutricionalServicio : GenericoServicio {
        PsConsumoNutricional obtenerPorId(PsConsumoNutricionalPk id);
        PsConsumoNutricional obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdConsumo,Nullable<Int32> pIdConsumoNutricional);
        PsConsumoNutricional coreActualizar(UsuarioActual usuarioActual, PsConsumoNutricional bean);
        PsConsumoNutricional coreInsertar(UsuarioActual usuarioActual, PsConsumoNutricional bean);
        void coreEliminar(PsConsumoNutricionalPk id);
        void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdConsumo, Nullable<Int32> pIdConsumoNutricional);
     
        List<DtoConsumoNutricional> listardetalle(PsConsumoPk llave);
    }
}
