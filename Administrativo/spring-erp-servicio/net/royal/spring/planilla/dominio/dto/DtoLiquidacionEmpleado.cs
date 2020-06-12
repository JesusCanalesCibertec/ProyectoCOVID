using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoLiquidacionEmpleado
    {
       public Nullable<Int32> idEmpleado{ get; set; }
       public String nombreEmpleado{ get; set; }
       public Nullable<DateTime> fechaIngreso{ get; set; }
       public Nullable<DateTime> fechaCese{ get; set; }
       public Nullable<DateTime> fechaLiquidacion{ get; set; }
       public Nullable<Decimal> totalIngresos{ get; set; }
       public Nullable<Decimal> totalDescuentos{ get; set; }
       public Nullable<Decimal> montoLiquidacion{ get; set; }
       public String companiaSocio{ get; set; }
        public String textoReporte { get; set; }
    }
}
