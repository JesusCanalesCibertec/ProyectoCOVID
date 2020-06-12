using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsEntidadProgramaSocialServicio : GenericoServicio {
        PsEntidadProgramaSocial obtenerPorId(PsEntidadProgramaSocialPk id);
        PsEntidadProgramaSocial obtenerPorId(Nullable<Int32> pIdEntidad,String pIdProgramaSocial);
        PsEntidadProgramaSocial coreActualizar(UsuarioActual usuarioActual, PsEntidadProgramaSocial bean);
        PsEntidadProgramaSocial coreInsertar(UsuarioActual usuarioActual, PsEntidadProgramaSocial bean);
        void coreEliminar(PsEntidadProgramaSocialPk id);
        void coreEliminar(Nullable<Int32> pIdEntidad,String pIdProgramaSocial);
    }
}
