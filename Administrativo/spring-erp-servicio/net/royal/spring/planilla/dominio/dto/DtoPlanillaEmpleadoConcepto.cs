using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoPlanillaEmpleadoConcepto
    {
        public String conceptoIdTipo{ get; set; }
        public String conceptoId{ get; set; }
        public String conceptoNombre{ get; set; }
        public Nullable<Decimal> monto{ get; set; }
        public Nullable<Decimal> cantidad{ get; set; }
        public Nullable<Decimal> saldo{ get; set; }
        public Nullable<Decimal> montoExtranjera{ get; set; }
        public Nullable<Decimal> cantidadExtranjera{ get; set; }
        public Nullable<Decimal> saldoExtranjera{ get; set; }
    }
}
