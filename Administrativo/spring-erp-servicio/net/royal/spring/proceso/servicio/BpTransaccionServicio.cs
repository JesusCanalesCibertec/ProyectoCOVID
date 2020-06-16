using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.servicio
{
    public interface BpTransaccionServicio : GenericoServicio
    {
        BpTransaccion obtenerPorId(Int32 idTransaccion);
        List<DtoTransaccionUsuario> listarTransaccionSeguimiento(UsuarioActual usuarioActual, Int32 idTransaccion, String idProceso);
        string obtenerContenidoCorreo(int v, int? idTransaccion);
        List<DtoTransaccionNotificacion> listarTransaccionesPorUsuario(UsuarioActual usuarioActual, string idProceso);
    }
}
