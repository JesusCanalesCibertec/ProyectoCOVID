using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.minedu.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.dao
{
    public interface MpContratacionadendaentregableDao : GenericoDao<MpContratacionadendaentregable>
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpContratacionadendaentregable registrar(UsuarioActual usuarioActual, MpContratacionadendaentregable bean);
        MpContratacionadendaentregable actualizar(UsuarioActual usuarioActual, MpContratacionadendaentregable bean);
        List<MpContratacionadendaentregable> listado(DtoTabla filtro);
        List<MpContratacionadendaentregable> listado(int? pIdContratacion);

        void eliminarDetalle(int? pIdCoontratacion);
    }
}
