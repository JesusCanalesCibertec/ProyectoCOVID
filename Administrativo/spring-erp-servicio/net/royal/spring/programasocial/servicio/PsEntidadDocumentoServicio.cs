using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsEntidadDocumentoServicio : GenericoServicio {
        PsEntidadDocumento obtenerPorId(PsEntidadDocumentoPk id);
        PsEntidadDocumento obtenerPorId(Nullable<Int32> pIdEntidad,String pIdTipoDocumento);
        PsEntidadDocumento coreActualizar(UsuarioActual usuarioActual, PsEntidadDocumento bean);
        PsEntidadDocumento coreInsertar(UsuarioActual usuarioActual, PsEntidadDocumento bean);
        void coreEliminar(PsEntidadDocumentoPk id);
        void coreEliminar(Nullable<Int32> pIdEntidad,String pIdTipoDocumento);
    }
}
