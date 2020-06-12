using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoAuditoria
    {
        public String Periodo { get; set; }
        public String CompaniaSocioId { get; set; }
        public String CompaniaSocioNombre { get; set; }
        public String CentroCostoId { get; set; }
        public String CentroCostoNombre { get; set; }
        public String SucursalId { get; set; }
        public String SucursalNombre { get; set; }
        public String ProcesoId { get; set; }
        public String ProcesoNombre { get; set; }
        public String FuncionalidadId { get; set; }
        public String FuncionalidadNombre { get; set; }
        public String AreaId { get; set; }
        public String AreaNombre { get; set; }
        public Int32? EmpleadoId { get; set; }
        public String EmpleadoNombre { get; set; }
        public String EmpleadoDocumento { get; set; }
        public String fecha { get; set; }
        public String planillaNombre { get; set; }
    }
}
