using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.asistencia.servicio
{

    public interface AsPeriodoServicio : GenericoServicio
    {
        List<AsPeriodo> listarTodos();
        bool esPeriodoAcitvo(Decimal empleado, DateTime fechadesde, DateTime fechahasta);
        AsPeriodo obtenerPorId(AsPeriodoPk pk);
        String obtenerPeriodo(int empleado);
    }
}
