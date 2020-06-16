using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.servicio.evento
{

    public interface BpEjecutarServicio
    {
        void ejecutar(UsuarioActual usuarioActual, BpTransaccion bpTransaccion, BpProcesoconexion bpProcesoConexion);
    }
}
