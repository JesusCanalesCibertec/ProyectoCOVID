using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.rrhh.dominio.dtoscomponente;

namespace net.royal.spring.programasocial.servicio
{
    public interface PsFurhServicio : GenericoServicio
    {
        List<DtoComponenteFurh> consultar(UsuarioActual usuarioActual, FiltroFurh filtro);
        DtoComponenteFurh obtenerPorId(String idInstitucion, Nullable<Int32> idEmpleado);
        DtoComponenteFurh actualizar(UsuarioActual usuarioActual, DtoComponenteFurh bean);
        DtoComponenteFurh insertar(UsuarioActual usuarioActual, DtoComponenteFurh bean);
        DtoComponenteFurh anular(UsuarioActual usuarioActual, String idInstitucion, Nullable<Int32> idEmpleado);
        ParametroPaginacionGenerico listarFurh(ParametroPaginacionGenerico paginacion, DtoComponenteFurh filtro);
        string Exportar(DtoComponenteFurh filtro);
    }
}
