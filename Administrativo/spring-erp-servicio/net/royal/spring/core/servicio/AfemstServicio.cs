using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio
{

    public interface AfemstServicio : GenericoServicio
    {
        List<Afemst> listarTodos();

        List<Afemst> listar(FiltroAfe filtro);

        List<Afemst> listarActivos();

        Afemst obtenerporid(AfemstPk pk);

        ParametroPaginacionGenerico listarPaginacion(DtoTabla filtro, ParametroPaginacionGenerico paginacion);
    }
}
