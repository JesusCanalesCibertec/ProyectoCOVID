using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;

namespace net.royal.spring.proceso.servicio
{
    public interface BpMaeprocesoServicio : GenericoServicio
    {

        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroBpMaeproceso filtro);

      
        BpMaeproceso obtenerPorId(string codigo);
        BpMaeproceso coreActualizar(UsuarioActual usuarioActual, BpMaeproceso bean);
        BpMaeproceso coreInsertar(UsuarioActual usuarioActual, BpMaeproceso bean);
        List<BpMaeproceso> listarTodos();
    }
}
