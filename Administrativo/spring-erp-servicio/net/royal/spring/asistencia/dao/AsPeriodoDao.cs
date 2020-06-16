using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.asistencia.dao
{

    public interface AsPeriodoDao : GenericoDao<AsPeriodo>
    {
        AsPeriodo obtenerPorId(AsPeriodoPk pk);
        bool esPeriodoAcitvo(Decimal empleado, DateTime fechadesde, DateTime fechahasta);
        AsPeriodo obtenerPorEmpleado(int? solicitanteId);
        List<AsPeriodo> obtenerPorEmpleadoRangoFecha(DateTime? fecha, DateTime? fechafin, int? solicitanteId);
        String obtenerPeriodo(int empleado);
    }
}
