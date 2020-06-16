using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoSolicitudCapacitacion
    {
        public String comapnyowner { get; set; }
        public String capacitacionId { get; set; }
        public Nullable<Int32> cursoId { get; set; }
        public String cursoNombre { get; set; }
        public String tipocursoId { get; set; }
        public String tipocursoNombre { get; set; }
        public Nullable<Int32> solicitanteId { get; set; }
        public String solicitanteNombre { get; set; }
        public String fechaSolicitud { get; set; }
        public Nullable<DateTime> fechaDesde { get; set; }
        public Nullable<DateTime> fechaHasta { get; set; }
        public String monedaId { get; set; }
        public String monedaSigla { get; set; }
        public Nullable<Decimal> costoTotalEstimadoLocal { get; set; }
        public Nullable<Decimal> costoTotalEstimadoExtranjero { get; set; }
        public String estadoId { get; set; }
        public String estadoNombre { get; set; }
        public int numeroProceso { get; set; }
    }
}
