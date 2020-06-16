using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema.servicio
{

    public interface SySeguridadautorizacionesServicio : GenericoServicio {
        List<DtoTabla> listarEmpresasPorUsuario(String idUsuario);
        List<DtoTabla> listarEmpresasPorUsuarioEnEmpleadoYSeguridadAutorizacion(String idUsuario);
        List<DtoTabla> listarPorGrupoYUsuario(String idUsuario);
    }
}
