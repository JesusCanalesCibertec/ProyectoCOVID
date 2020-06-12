using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema.dao
{

    public interface SySeguridadautorizacionesDao : GenericoDao<SySeguridadautorizaciones>
    {
        List<DtoTabla> listarEmpresasPorUsuario(string idUsuario);
        List<DtoTabla> listarEmpresasPorUsuarioEnEmpleadoYSeguridadAutorizacion(String idUsuario);
        List<DtoTabla> listarPorGrupoYUsuario(String idUsuario);
    }
}
