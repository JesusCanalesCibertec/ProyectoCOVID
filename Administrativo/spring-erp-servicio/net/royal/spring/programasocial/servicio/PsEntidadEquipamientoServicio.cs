using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsEntidadEquipamientoServicio : GenericoServicio {
        PsEntidadEquipamiento obtenerPorId(PsEntidadEquipamientoPk id);
        PsEntidadEquipamiento obtenerPorId(Nullable<Int32> pIdEntidad,String pIdEquipamiento);
        PsEntidadEquipamiento coreActualizar(UsuarioActual usuarioActual, PsEntidadEquipamiento bean);
        PsEntidadEquipamiento coreInsertar(UsuarioActual usuarioActual, PsEntidadEquipamiento bean);
        void coreEliminar(PsEntidadEquipamientoPk id);
        void coreEliminar(Nullable<Int32> pIdEntidad,String pIdEquipamiento);
    }
}
