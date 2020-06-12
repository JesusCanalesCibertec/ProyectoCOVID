using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsEntidadServicio : GenericoServicio {
        PsEntidad obtenerPorId(PsEntidadPk id);
        PsEntidad obtenerPorId(Nullable<Int32> pIdEntidad);
        PsEntidad coreActualizar(UsuarioActual usuarioActual, PsEntidad bean);
        PsEntidad coreInsertar(UsuarioActual usuarioActual, PsEntidad bean);
        void coreEliminar(PsEntidadPk id);
        void coreEliminar(Nullable<Int32> pIdEntidad);
        PsEntidad coreAnular(UsuarioActual usuarioActual,PsEntidadPk id);
        PsEntidad coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pIdEntidad);
    }
}
