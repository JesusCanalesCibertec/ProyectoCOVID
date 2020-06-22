using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.covid.dominio;
using System.Collections.Generic;
using net.royal.spring.covid.dominio.filtro;

namespace net.royal.spring.covid.servicio
{
    public interface ResultadoServicio : GenericoServicio
    {
        //ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroResultado filtro);
        Resultado registrar(UsuarioActual usuarioActual, Resultado bean);
        Resultado obtenerPorId(int pIdResultado);
        Resultado actualizar(UsuarioActual usuarioActual, Resultado bean);
        ResultadoPk cambiarestado(ResultadoPk pk);
        List<Resultado> listado(DtoTabla filtro);
    }
}
