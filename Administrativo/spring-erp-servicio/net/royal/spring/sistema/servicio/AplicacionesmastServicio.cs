using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema.servicio
{

    public interface AplicacionesmastServicio : GenericoServicio {
        List<Aplicacionesmast> ListarActivos();
        string obtenerPeriodoContable(string aplicacionCodigo);
    }
}
