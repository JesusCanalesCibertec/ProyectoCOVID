using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.dao
{
    public interface BpTransaccionDao : GenericoDao<BpTransaccion>
    {
        List<DtoTransaccionUsuario> listarTransaccionSeguimiento(UsuarioActual usuarioActual, int idTransaccion, string idProceso);
        string obtenerContenidoCorreo(int tipo, int? idTransaccion);
        int? generarIdTransaccion();
        List<DtoTransaccionNotificacion> listarTransaccionesPorUsuario(UsuarioActual usuarioActual, string idProceso);
    }
}
