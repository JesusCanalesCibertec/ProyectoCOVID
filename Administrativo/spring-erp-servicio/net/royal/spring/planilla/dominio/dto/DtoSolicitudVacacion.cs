using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoSolicitudVacacion
    {
        public Nullable<Int32> numeroSolicitud{ get; set; }
        public Nullable<Int32> empleadoId{ get; set; }
        public String empleadoNombre{ get; set; }
        public Nullable<DateTime> fechaRegistro{ get; set; }
        public Nullable<DateTime> fechaInicio{ get; set; }
        public Nullable<DateTime> fechaFin{ get; set; }
        public Nullable<Int32> dias{ get; set; }
        public Nullable<Int32> solicitanteId{ get; set; }
        public String solicitanteNombre{ get; set; }
        public String estadoId{ get; set; }
        public String estadoNombre{ get; set; }
        public String aprobadorNombre { get; set; }
        public String flagConvenio { get; set; }
    }
}
