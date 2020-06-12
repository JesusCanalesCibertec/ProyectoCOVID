using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoBoletaEmpleado
    {
        public string tipoActividad;

        public DtoBoletaEmpleado()
        {

        }

        public DtoBoletaEmpleado(Int32 fila)
        {
            this.fila = fila;
        }

        public string nacionalidad { get; set; }
        public Nullable<Int32> fila { get; set; }
        public String ingrConceptoIdTipo { get; set; }
        public String ingrConceptoId { get; set; }
        public String ingrConceptoNombre { get; set; }
        public Nullable<Decimal> ingrMonto { get; set; }
        public Nullable<Decimal> ingrCantidad { get; set; }
        public Nullable<Decimal> ingrSaldo { get; set; }
        public Nullable<Decimal> ingrMontoExtranjera { get; set; }
        public Nullable<Decimal> ingrCantidadExtranjera { get; set; }
        public Nullable<Decimal> ingrSaldoExtranjera { get; set; }
        public String descConceptoIdTipo { get; set; }
        public String descConceptoId { get; set; }
        public String descConceptoNombre { get; set; }
        public Nullable<Decimal> descMonto { get; set; }
        public Nullable<Decimal> descCantidad { get; set; }
        public Nullable<Decimal> descSaldo { get; set; }
        public Nullable<Decimal> descMontoExtranjera { get; set; }
        public Nullable<Decimal> descCantidadExtranjera { get; set; }
        public Nullable<Decimal> descSaldoExtranjera { get; set; }
        public String patrConceptoIdTipo { get; set; }
        public String patrConceptoId { get; set; }
        public String patrConceptoNombre { get; set; }
        public Nullable<Decimal> patrMonto { get; set; }
        public Nullable<Decimal> patrCantidad { get; set; }
        public Nullable<Decimal> patrSaldo { get; set; }
        public Nullable<Decimal> patrMontoExtranjera { get; set; }
        public Nullable<Decimal> patrCantidadExtranjera { get; set; }
        public Nullable<Decimal> patrSaldoExtranjera { get; set; }
        public String reteConceptoIdTipo { get; set; }
        public String reteConceptoId { get; set; }
        public String reteConceptoNombre { get; set; }
        public Nullable<Decimal> reteMonto { get; set; }
        public Nullable<Decimal> reteCantidad { get; set; }
        public Nullable<Decimal> reteSaldo { get; set; }
        public Nullable<Decimal> reteMontoExtranjera { get; set; }
        public Nullable<Decimal> reteCantidadExtranjera { get; set; }
        public Nullable<Decimal> reteSaldoExtranjera { get; set; }

        //Datos de la Boleta
        public String monedaPago { get; set; }
        public Nullable<DateTime> fechaIngresoBoleta { get; set; }
        public Nullable<DateTime> fechaCeseBoleta { get; set; }
        public String estadoEmpleado { get; set; }
        public Int32 codigoCargo { get; set; }
        public String puestoDescripcion { get; set; }
        public String dptoOrganizacion { get; set; }
        public String numeroAfp { get; set; }
        public String gradoSalario { get; set; }
        public String tipoTrabajador { get; set; }
        public String apellidoPaterno { get; set; }
        public String apellidoMaterno { get; set; }
        public String nombres { get; set; }
        public String documentoIdentidad { get; set; }
        public String documento { get; set; }
        public String direccion { get; set; }
        public String codigoAfp { get; set; }
        public String compania { get; set; }
        public Nullable<Decimal> totalIngresos { get; set; }
        public Nullable<Decimal> totalEgresos { get; set; }
        public Decimal totalPatronales { get; set; }
        public Nullable<Decimal> totalNeto { get; set; }
        public Nullable<DateTime> vacacionDesde { get; set; }
        public Nullable<DateTime> vacacionHasta { get; set; }
        public String centroCosto { get; set; }
        public String centroCostodescripcion { get; set; }
        public String afe { get; set; }
        public Int32 empleado { get; set; }
        public Decimal sueldoBasicoLocal { get; set; }
        public Decimal essalud { get; set; }
        public Decimal epsempresa { get; set; }
        public Decimal sueldoBasicoDolar { get; set; }
        public Nullable<Int32> diasTrabajados { get; set; }
        public Nullable<Int32> DiasNoTrabNoSub { get; set; }
        public Nullable<Int32> diasSubsidiados { get; set; }
        public Int32 horasTrabajadas { get; set; }
        public Decimal tipoCambio { get; set; }
        public String locaciondePago { get; set; }
        public String departamentoOperacional { get; set; }
        public String nombreCompleto { get; set; }
        public String registroPatronalPlanilla { get; set; }
        public String cuenta { get; set; }
        public String banco { get; set; }
        public String monedaBanco { get; set; }
        public String nombreAfp { get; set; }
        public String tipoContrato { get; set; }
        public String tipoContratoNombre { get; set; }
        public String comentario { get; set; }
        public String flagConfianza { get; set; }
        public String documentofiscal { get; set; }
        public String direccionadicional { get; set; }
        public String direccioncomun { get; set; }
        public String TipoPlanilla { get; set; }
        public String desbanco { get; set; }
        public Decimal TotalIngresosExtranjera { get; set; }
        public Decimal TotalEgresosExtranjera { get; set; }
        public Decimal TotalPatronalesExtranjera { get; set; }
        public Decimal TotalNetoExtranjera { get; set; }
        public String sucursal { get; set; }
        public String tipoPlanillaNombre { get; set; }
        public String responsableFirma { get; set; }
        public String monedaNombre { get; set; }

        public String departamento { get; set; }
        public String provincia { get; set; }
        public String distrito { get; set; }
        public String periodoinicio { get; set; }
        public String periodofin { get; set; }

    }
}
