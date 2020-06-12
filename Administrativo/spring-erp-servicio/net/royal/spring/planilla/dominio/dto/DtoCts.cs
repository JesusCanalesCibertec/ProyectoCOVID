using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoCts
    {
        public Int32 numeroCts { get; set; }
        public String ctsDescripcion { get; set; }
        public Nullable<DateTime> fechaPago { get; set; }
        public Nullable<DateTime> fechaInicio { get; set; }
        public Nullable<DateTime> fechaFin { get; set; }
        public Nullable<Decimal> indemnizacionAnual { get; set; }
        public Decimal tipoCambio { get; set; }
        public Nullable<Decimal> montoLocal { get; set; }
        public Decimal montoDolar { get; set; }
        public Decimal remuneracion { get; set; }
        public String bancoNombre { get; set; }
        public String monedaId { get; set; }
        public String numeroCuentaCts { get; set; }
        public String tiempoACancelar { get; set; }
        public Int32 empleado { get; set; }
        public String nombreCompleto { get; set; }
        public String personaAnt { get; set; }
        public Nullable<DateTime> fechaInicioContrato { get; set; }
        public Decimal iml { get; set; }
        public Decimal  tiempoValorizado { get; set; }
        public String descripcion { get; set; }
        public Decimal detallemontolocal { get; set; }
        public String estadoCts { get; set; }
        public Nullable<DateTime> prFechaPago { get; set; }
        public Nullable<DateTime> maFechaPago { get; set; }
        public Nullable<DateTime> fechaIngreso { get; set; }
        public Nullable<DateTime> fechaCese { get; set; }
        public String estadoEmpleado { get; set; }
        public String unidadNegocioAsignada { get; set; }
        public String tipoCancelacion { get; set; }
        public String centroCostos { get; set; }
        public String cargo { get; set; }
        public String registroPatronalPlanilla { get; set; }
        public String documento { get; set; }
        public String empleador { get; set; }
        public String ruc { get; set; }
        public String direccion { get; set; }
        public Int32 codigoCargo { get; set; }
        public Nullable<Decimal> montoInteres { get; set; }
        public Nullable<Decimal> retencionJudicial { get; set; }
        public String bancoDescripcion { get; set; }
        public Int32 secuencia { get; set; }
        public Nullable<Decimal> montoCancelar { get ; set; }
        public Nullable<Decimal> montoDepositar { get; set; }
        public String banco { get; set; }
        public String monedaCuentaCTS { get; set; }
        public String monedaDescripcionLargaCuentaCTS { get; set; }
        public int? detalle { get; set; }
    }
}
