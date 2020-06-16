using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.dao
{
    public interface MpPersonatituloDao : GenericoDao<MpPersonatitulo>
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpPersonatitulo registrar(UsuarioActual usuarioActual, MpPersonatitulo bean);
        MpPersonatitulo actualizar(UsuarioActual usuarioActual, MpPersonatitulo bean);
        List<DtoPersonatitulo> listado(int? pIdPersona);
    }
}
