using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.minedu.dominio;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema.dao
{

    public interface SeguridadperfilusuarioDao : GenericoDao<Seguridadperfilusuario>
    {
        List<Seguridadperfilusuario> listarPerfilesActivos(String idUsuario);
        ParametroPaginacionGenerico ListarPaginacionUsuario(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        Seguridadperfilusuario coreInsertar(UsuarioActual usuarioActual, Seguridadperfilusuario bean, string estado);
    }
}
