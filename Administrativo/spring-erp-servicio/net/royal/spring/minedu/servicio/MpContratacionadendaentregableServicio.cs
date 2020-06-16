using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.minedu.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.servicio
{
    public interface MpContratacionadendaentregableServicio : GenericoServicio
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpContratacionadendaentregable registrar(UsuarioActual usuarioActual, MpContratacionadendaentregable bean);
        MpContratacionadendaentregable obtenerPorId(int pIdContratacionadendaentregable);
        MpContratacionadendaentregable actualizar(UsuarioActual usuarioActual, MpContratacionadendaentregable bean);
        MpContratacionadendaentregablePk cambiarestado(MpContratacionadendaentregablePk pk);
        List<MpContratacionadendaentregable> listado(DtoTabla filtro);
        List<MpContratacionadendaentregable> listado(int? pIdContratacion);
        
    }
}
