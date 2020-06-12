using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.planilla.dominio;

namespace net.royal.spring.planilla.servicio
{

    public interface PrCalendarioferiadosServicio : GenericoServicio
    {
        IList<PrCalendarioferiados> listarActivos();
    }
}
