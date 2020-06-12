using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.servicio
{
    public interface MpPersonatituloServicio : GenericoServicio
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpPersonatitulo registrar(UsuarioActual usuarioActual, MpPersonatitulo bean);
        MpPersonatitulo obtenerPorId(int pIdPersona);
        MpPersonatitulo actualizar(UsuarioActual usuarioActual, MpPersonatitulo bean);
        MpPersonatituloPk cambiarestado(MpPersonatituloPk pk);
        List<DtoPersonatitulo> listado(int? pIdPersona);
        void eliminar(MpPersonatitulo bean);
        MpPersonatitulo obtenerPorId(MpPersonatituloPk pk);
    }
}
