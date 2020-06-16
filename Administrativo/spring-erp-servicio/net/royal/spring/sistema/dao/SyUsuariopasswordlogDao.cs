using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dominio.dto;

namespace net.royal.spring.sistema.dao
{

    public interface SyUsuariopasswordlogDao : GenericoDao<SyUsuariopasswordlog>
    {
        DtoUsuarioPasswordLog obtenerUltimoLogin(String idUsuario);
    }
}
