using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.dao
{

    public interface AfemstDao : GenericoDao<Afemst>
    {
        Afemst obtenerPorId(AfemstPk pk);
        List<Afemst> listar(FiltroAfe filtro);
        ParametroPaginacionGenerico listarPaginacion(DtoTabla filtro, ParametroPaginacionGenerico paginacion);
    }
}
