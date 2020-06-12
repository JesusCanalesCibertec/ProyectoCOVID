using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.proyecto.dominio;

namespace net.royal.spring.proyecto.servicio
{

    public interface PmTipoproyectoServicio : GenericoServicio {
        PmTipoproyecto obtenerPorId(String id);
    }
}
