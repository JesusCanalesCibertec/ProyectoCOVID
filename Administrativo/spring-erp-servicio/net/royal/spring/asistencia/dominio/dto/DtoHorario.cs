using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoHorario
    {
        public Decimal Empleado { get; set; }
        public Nullable<DateTime> Fecha { get; set; }
        public Nullable<DateTime> FechaHasta { get; set; }
        public String Intervaloacceso { get; set; }
        public Decimal Tipohorario { get; set; }
        public Decimal DerechoVacacional { get; set; }

    }
}
