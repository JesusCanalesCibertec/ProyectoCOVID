using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema.servicio
{

    public interface SeguridadautorizacionesServicio : GenericoServicio {
        List<DtoTabla> listarAplicacionPorUsuario(String idUsuario);
        List<Seguridadgrupo> listarPorAplicacionUsuario(String idAplicacion, String idUsuario);
        string esRRHH(String idUsuario);

    }
}
