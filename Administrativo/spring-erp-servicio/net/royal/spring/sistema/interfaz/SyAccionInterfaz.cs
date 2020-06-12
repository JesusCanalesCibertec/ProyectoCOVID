using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.interfaz
{
    public interface SyAccionInterfaz
    {
        DtoSolicitud ejecutarAccion(UsuarioActual usuarioActual, String accion, DtoSolicitud solicitud);

        DtoSolicitud obtener(String accion, DtoSolicitud solicitud);

        List<ParametroPersistenciaGenerico> obtenerMetadata(String accion, DtoSolicitud solicitud);

        MensajeUsuario validarPreAccion(UsuarioActual usuarioActual, string accion, DtoSolicitud bean);

    }
}
