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
    public interface CiudadanoDao : GenericoDao<Ciudadano>
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroCiudadano filtro);
        Ciudadano registrar(UsuarioActual usuarioActual, Ciudadano bean);
        Ciudadano actualizar(UsuarioActual usuarioActual, Ciudadano bean);
        List<Ciudadano> listado(DtoTabla filtro);

    }
}
