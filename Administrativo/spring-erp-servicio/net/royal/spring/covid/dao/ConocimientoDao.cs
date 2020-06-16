using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.covid.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.covid.dao
{
    public interface ConocimientoDao : GenericoDao<Conocimiento>
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        Conocimiento registrar(UsuarioActual usuarioActual, Conocimiento bean);
        Conocimiento actualizar(UsuarioActual usuarioActual, Conocimiento bean);
        List<Conocimiento> listado(DtoTabla filtro);

    }
}
