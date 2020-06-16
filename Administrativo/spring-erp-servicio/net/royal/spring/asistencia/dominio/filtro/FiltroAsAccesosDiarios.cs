using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.asistencia.dominio.filtro
{
    public class FiltroAsAccesosDiarios : DominioOrden
    {
        public DateTime? fechaDesde { get; set; }
        public DateTime? fechaHasta { get; set; }
        public Int32 Empleado { get; set; }
        public String EmpleadoNombre { get; set; }
        public String EmpleadoConcepto { get; set; }
        public String EmpledoEstado { get; set; }
        public String EmpleadoCompania { get; set; }

        public String HorasExtrasAcumuladas { get; set; }
        public String HorasDebeAcumuladas { get; set; }
        public String HorasExtrasRango { get; set; }
        public String HorasDebeRango { get; set; }
        public Int32 Periodo { get; set; }
        public String IdUnidadOperativa { get; set; }

    }
}
