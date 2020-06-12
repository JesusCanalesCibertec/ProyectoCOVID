using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.planilla.dominio.dto
{
    public class DtoPlanillaEmpleado
    {
        public String companiaSocioId { get; set; }
        public String companiaSocioNombre { get; set; }

        public Int32 empleadoId { get; set; }
        public String empleadoNombre { get; set; }

        public String periodoId { get; set; }
        public String periodoNombre { get; set; }

        public String tipoPlanillaId { get; set; }
        public String tipoPlanillaNombre { get; set; }

        public String tipoProcesoId { get; set; }
        public String tipoProcesoNombre { get; set; }

        public Nullable<Decimal> totalIngresos { get; set; }
        public Nullable<Decimal> totalEgresos { get; set; }
        public Nullable<Decimal> totalNeto { get; set; }
        public Nullable<Decimal> totalPatronales { get; set; }

        //detalle boleta
        public String conceptoIdIn { get; set; }
        public String conceptoNombreIn { get; set; }
        public String conceptoTipoIn { get; set; }
        public Nullable<Double> conceptoMontoIn { get; set; }
        public Nullable<Decimal> conceptoCantidadIn { get; set; }
        public Double conceptoSaldoIn { get; set; }

        public String conceptoIdDe { get; set; }
        public String conceptoNombreDe { get; set; }
        public String conceptoTipoDe { get; set; }
        public Nullable<Double> conceptoMontoDe { get; set; }
        public Nullable<Decimal> conceptoCantidadDe { get; set; }
        public Double conceptoSaldoDe { get; set; }

        public String conceptoIdPa { get; set; }
        public String conceptoNombrePa { get; set; }
        public String conceptoTipoPa { get; set; }
        public Nullable<Double> conceptoMontoPa { get; set; }
        public Nullable<Decimal> conceptoCantidadPa { get; set; }
        public Double conceptoSaldoPa { get; set; }

        public Double tipoCambio { get; set; }
    }
}
