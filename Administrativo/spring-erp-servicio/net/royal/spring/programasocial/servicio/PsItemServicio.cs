using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsItemServicio : GenericoServicio {
        PsItem obtenerPorId(PsItemPk id);
        PsItem obtenerPorId(String pIdItem);
        PsItem coreActualizar(UsuarioActual usuarioActual, PsItem bean);
        PsItem coreInsertar(UsuarioActual usuarioActual, PsItem bean);
        void coreEliminar(PsItemPk id);
        void coreEliminar(String pIdItem);
        PsItem coreAnular(UsuarioActual usuarioActual,PsItemPk id);
        PsItem coreAnular(UsuarioActual usuarioActual,String pIdItem);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroItem filtro);
        void eliminar(string idItem);
    }
}
