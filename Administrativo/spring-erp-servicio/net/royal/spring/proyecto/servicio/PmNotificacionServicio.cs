using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dominio.dto;

namespace net.royal.spring.proyecto.servicio
{

    public interface PmNotificacionServicio : GenericoServicio
    {
        void eliminarNotificacionesPorTransaccion(UsuarioActual usuarioActual, int? idTransaccion);
        void generarNotificacionExterna(UsuarioActual usuarioActual, DtoInterfazNotificacion dtoInterfazNotificacion);
        List<PmNotificacion> listarNotificacionesPorPersona(UsuarioActual usuarioActual);
    }
}
