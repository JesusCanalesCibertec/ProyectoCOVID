using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.covid.dominio;
using System.Collections.Generic;
using net.royal.spring.covid.dominio.filtro;

namespace net.royal.spring.covid.servicio
{
    public interface TriajeServicio : GenericoServicio
    {
        //ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroTriaje filtro);
        Triaje registrar(UsuarioActual usuarioActual, Triaje bean);
        Triaje obtenerPorId(int pIdTriaje);
        Triaje actualizar(UsuarioActual usuarioActual, Triaje bean);
        TriajePk cambiarestado(TriajePk pk);
        List<Triaje> listado(DtoTabla filtro);
        List<DtoTabla> listado(int pIdCiudadano);
    }
}
