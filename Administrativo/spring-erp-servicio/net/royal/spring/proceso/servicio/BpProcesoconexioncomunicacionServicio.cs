using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.proyecto.dominio.dto;

namespace net.royal.spring.proceso.servicio
{
    public interface BpProcesoconexioncomunicacionServicio : GenericoServicio
    {
        List<DtoTransaccionUsuario> listarUsuariosPorProcesoConexion(BpProcesoconexionPk bpProcesoconexionPk, string tIPO_COMUNICACION_CORREO);
    }
}
