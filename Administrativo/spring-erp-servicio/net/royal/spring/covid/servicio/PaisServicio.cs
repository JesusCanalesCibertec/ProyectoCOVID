using System;
using System.Collections.Generic;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.covid.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.covid.servicio
{

public interface PaisServicio : GenericoServicio {
        List<Pais> listarTodos();

        DtoTabla obtenerUbigeo(String ubigeo);
        ParametroPaginacionGenerico listarUbigeoPorFiltro(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
    }
}
