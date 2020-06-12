using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema.dao
{

public interface SeguridadautorizacionesDao : GenericoDao<Seguridadautorizaciones>
{
        List<DtoTabla> listarAplicacionPorUsuario(String idUsuario);
        List<Seguridadgrupo> listarPorAplicacionUsuario(String idAplicacion, String idUsuario);
        string esRRHH(String idUsuario);

    }
}
