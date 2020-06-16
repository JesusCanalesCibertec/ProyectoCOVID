using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoPrPrestamoListado
    {
        public Int64 correlativo { get; set; }
        public String companiasocio { get; set; }
        public Nullable<Int32> nroprestamo { get; set; }
        public String tipoprestamo { get; set; }
        public String tipoprestamoNombre { get; set; }
        public Nullable<DateTime> fechaSolicitud { get; set; }
        public Nullable<DateTime> fechaAprobacion { get; set; }
        public String monedaPago { get; set; }
        public String monedaPagoNombre { get; set; }
        public String monedaSigla { get; set; }
        public Nullable<Decimal> montoPrestamo { get; set; }
        public Nullable<Decimal> montoPagado { get; set; }
        public Nullable<Decimal> saldoDeuda { get; set; }
        public String estado { get; set; }
        public String estadoNombre { get; set; }
        public Nullable<Int32> solicitanteId { get; set; }
        public String solicitanteNombre { get; set; }
        public Nullable<Int32> numeroproceso { get; set; }
      
    }
}
