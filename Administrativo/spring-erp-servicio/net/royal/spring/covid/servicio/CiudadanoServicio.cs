using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.covid.dominio;
using System.Collections.Generic;
using net.royal.spring.covid.dominio.filtro;

namespace net.royal.spring.covid.servicio
{
    public interface CiudadanoServicio : GenericoServicio
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroCiudadano filtro);
        Ciudadano registrar(UsuarioActual usuarioActual, Ciudadano bean);
        Ciudadano obtenerPorId(int pIdCiudadano);
        Ciudadano actualizar(UsuarioActual usuarioActual, Ciudadano bean);
        CiudadanoPk cambiarestado(CiudadanoPk pk);
        List<Ciudadano> listado(DtoTabla filtro);
        List<Ciudadano> listado();
        List<DtoTabla> ListarPie();
        List<DtoTabla> listarPiexDepartamento(string pDepa);
        List<DtoTabla> listarPiexProvincia(string pDepa, string pProv);
        List<DtoTabla> listarPiexDistrito(string pDepa, string pProv, string pDist);
    }
}
