using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.covid.dominio;
using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.covid.dominio.filtro;

namespace net.royal.spring.covid.dao
{
    public interface ResultadoDao : GenericoDao<Resultado>
    {
        //ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroResultado filtro);
        Resultado registrar(UsuarioActual usuarioActual, Resultado bean);
        Resultado actualizar(UsuarioActual usuarioActual, Resultado bean);
        List<Resultado> listado(DtoTabla filtro);

    }
}
