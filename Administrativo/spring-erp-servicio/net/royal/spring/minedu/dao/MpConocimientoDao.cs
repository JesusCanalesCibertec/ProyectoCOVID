using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.minedu.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.dao
{
    public interface MpConocimientoDao : GenericoDao<MpConocimiento>
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpConocimiento registrar(UsuarioActual usuarioActual, MpConocimiento bean);
        MpConocimiento actualizar(UsuarioActual usuarioActual, MpConocimiento bean);
        List<MpConocimiento> listado(DtoTabla filtro);

    }
}
