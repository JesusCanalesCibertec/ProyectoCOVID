using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.dao
{

    public interface MaCtsDao : GenericoDao<MaCts>
    {
        MaCts obtenerPorId(MaCtsPk pk);
        List<MaCts> listar(DtoFiltroEntero filtro);
    }
}
