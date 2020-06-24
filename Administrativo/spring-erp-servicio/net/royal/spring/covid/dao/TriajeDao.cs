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
    public interface TriajeDao : GenericoDao<Triaje>
    {
        //ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroTriaje filtro);
        Triaje registrar(UsuarioActual usuarioActual, Triaje bean);
        Triaje actualizar(UsuarioActual usuarioActual, Triaje bean);
        List<Triaje> listado(DtoTabla filtro);
        List<DtoTabla> listado(int pIdCiudadano);
    }
}
