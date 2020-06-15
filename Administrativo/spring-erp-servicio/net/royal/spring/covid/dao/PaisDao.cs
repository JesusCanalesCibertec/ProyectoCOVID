using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.covid.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.covid.dao
{

    public interface PaisDao : GenericoDao<Pais>
    {
        Pais obtenerPorId(PaisPk pk);

        DtoTabla obtenerUbigeo(String ubigeo);
        ParametroPaginacionGenerico listarUbigeoPorFiltro(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
    }
}
