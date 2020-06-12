using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoSolicitudGeneral
    {
        public Nullable<Int32> Numerosolicitud { get; set; }
        public Nullable<DateTime> FechaSolicitud { get; set; }
        public Nullable<Int32> EmpleadoSolicitanteId { get; set; }
        public String EmpleadoSolicitanteNombre { get; set; }
        public Nullable<Int32> TipoHorarioId { get; set; }
        public String TipoHorarioNombre { get; set; }        
        public String EstadoId { get; set; }
        public String EstadoNombre { get; set; }        
        public String codigoproceso { get; set; }
    }
}
