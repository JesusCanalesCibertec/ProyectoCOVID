using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.minedu.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.dao
{
    public interface MpPersonaconocimientoDao : GenericoDao<MpPersonaconocimiento>
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpPersonaconocimiento registrar(UsuarioActual usuarioActual, MpPersonaconocimiento bean);
        MpPersonaconocimiento actualizar(UsuarioActual usuarioActual, MpPersonaconocimiento bean);
        List<DtoTabla> listado(int? pIdPersona);
    }
}
