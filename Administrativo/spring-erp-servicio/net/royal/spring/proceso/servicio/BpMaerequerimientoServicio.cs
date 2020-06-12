using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;

namespace net.royal.spring.proceso.servicio
{
    public interface BpMaerequerimientoServicio : GenericoServicio
    {
        List<BpMaerequerimiento> listarTodos();
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroRequerimiento filtro);
        BpMaerequerimiento obtenerPorId(BpMaerequerimientoPk llave);
    }
}
