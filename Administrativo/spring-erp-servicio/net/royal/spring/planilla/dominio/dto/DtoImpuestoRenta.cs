using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{

    public class DtoImpuestoRenta
    {
        public String empleador { get; set; }
        public String responsable{ get; set; }
        public String documento{ get; set; }
        public String ruc{ get; set; }
        public String direccion{ get; set; }
        public String direccionTrabajador { get; set; }
        public String localidadTrabajor { get; set; }
        public String nombreCompleto { get; set; }
        public Nullable<Decimal> estimadoRentaTrabajo { get; set; }
        public Decimal sueldoActual { get; set; }
        public Decimal estimadoImpuestoMensual { get; set; }
        public Nullable<Decimal> acumuladoRetencion { get; set; }
        public Decimal acumuladoRetencionExterno { get; set; }
        public Nullable<Decimal> acumuladoSueldo { get; set; }
        public Decimal acumuladoSueldoExterno { get; set; }
        public Decimal impuestoAnualCalculado { get; set; }
        public Nullable<Decimal> estimadoImpuestoAnual { get; set; }
        public Decimal retencionExceso { get; set; }
        public Int32 empleado { get; set; }
        public String documentoIdentidad { get; set; }
        public Decimal deducciones { get; set; }
        public String ejercicioFiscal { get; set; }
        public DateTime fechaCese { get; set; }
        public String estadoEmpleado { get; set; }
        public DateTime fechaIngreso { get; set; }
        public String puesto { get; set; }
        public String tipoDocumento { get; set; }
        public String cargo { get; set; }
        public String centroCosto { get; set; }
        public Nullable<Decimal> devolucion { get; set; }
        public Decimal creditoFiscal { get; set; }
        public Decimal montoTope { get; set; }
        public Decimal impuesto { get; set; }  
    }


}
