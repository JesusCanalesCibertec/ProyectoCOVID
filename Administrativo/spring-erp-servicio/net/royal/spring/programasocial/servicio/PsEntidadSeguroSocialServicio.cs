using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsEntidadSeguroSocialServicio : GenericoServicio {
        PsEntidadSeguroSocial obtenerPorId(PsEntidadSeguroSocialPk id);
        PsEntidadSeguroSocial obtenerPorId(Nullable<Int32> pIdEntidad,String pIdSeguroSocial);
        PsEntidadSeguroSocial coreActualizar(UsuarioActual usuarioActual, PsEntidadSeguroSocial bean);
        PsEntidadSeguroSocial coreInsertar(UsuarioActual usuarioActual, PsEntidadSeguroSocial bean);
        void coreEliminar(PsEntidadSeguroSocialPk id);
        void coreEliminar(Nullable<Int32> pIdEntidad,String pIdSeguroSocial);
    }
}
