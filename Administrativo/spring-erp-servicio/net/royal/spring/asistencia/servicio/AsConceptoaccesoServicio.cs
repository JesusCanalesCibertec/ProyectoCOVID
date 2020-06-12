using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.asistencia.servicio
{

    public interface AsConceptoaccesoServicio : GenericoServicio
    {
        List<AsConceptoacceso> ListarActivos();
        List<AsConceptoacceso> ListarActivosOtros(bool flagWeb);
        AsConceptoacceso obtenerPorId(AsConceptoaccesoPk pk);
    }
}
