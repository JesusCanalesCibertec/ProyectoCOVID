using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoPermisos
    {
        public String tipoautorizacion { get; set; }
        public Nullable<Int32> codigo { get; set; }
        public String inicio { get; set; }
        public String fin { get; set; }
        public String concepto { get; set; }
        public String desde { get; set; }
        public String hasta { get; set; }
        public String autorizadopor { get; set; }
        public String nombre { get; set; }
        public String personaant { get; set; }
        public String docidentidad { get; set; }
        public String dias { get; set; }
        public Nullable<Int32> autogenerado { get; set; }
        public String conceptoacceso { get; set; }
        public String estado { get; set; }
        public String motivorechazo { get; set; }
        public Nullable<Int32> numeroproceso { get; set; }
        public String fechaSolicitud { get; set; }
        public Nullable<DateTime> fecha { get; set; }
        public Nullable<DateTime> desdeDate { get; set; }
        public String observacion { get; set; }

        public Nullable<Int32> empleado { get; set; }
        public String compania { get; set; }
        public String fecregistro { get; set; }
        public String unidad { get; set; }
        public String esJefe { get; set; }

        public String expresadoen { get; set; }
        public Nullable<DateTime> fechadesde { get; set; }
        public Nullable<DateTime> fechahasta { get; set; }
        public Nullable<DateTime> fechaRegistro { get; set; }
        public String comportamientoSobretiempo { get; set; }


    }
}
