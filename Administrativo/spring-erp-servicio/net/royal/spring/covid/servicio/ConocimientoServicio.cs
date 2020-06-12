using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.covid.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.covid.servicio
{
    public interface ConocimientoServicio : GenericoServicio
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        Conocimiento registrar(UsuarioActual usuarioActual, Conocimiento bean);
        Conocimiento obtenerPorId(int pIdConocimiento);
        Conocimiento actualizar(UsuarioActual usuarioActual, Conocimiento bean);
        ConocimientoPk cambiarestado(ConocimientoPk pk);
        List<Conocimiento> listado(DtoTabla filtro);
    }
}
