using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core.servicio
{

public interface TipocambiomastServicio : GenericoServicio {
        List<Tipocambiomast> listarTodos();
        Tipocambiomast obtenerPorFecha(DateTime fecha);
        Tipocambiomast obtenerPorFecha(String fechaCadena);
        float? obtenerFactorCompraPorFecha(String fechaCadena);
    }
}
