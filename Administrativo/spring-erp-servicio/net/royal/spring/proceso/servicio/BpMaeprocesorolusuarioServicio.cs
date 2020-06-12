using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.servicio
{
    public interface BpMaeprocesorolusuarioServicio : GenericoServicio
    {
        List<DtoBpMaeprocesorolusuario> listado(string pIdProceso, string pIdRol, string pIdTipoSeguridad);
        BpMaeprocesorolusuario obtenerPorId(BpMaeprocesorolusuarioPk llave);
        BpMaeprocesorolusuario coreActualizar(UsuarioActual usuarioActual, BpMaeprocesorolusuario bean);
        BpMaeprocesorolusuario coreInsertar(UsuarioActual usuarioActual, BpMaeprocesorolusuario bean);
        void eliminar(string pIdProceso, string pIdRol, string pIdTipoSeguridad, string pIdConcepto);
    }

    
}
