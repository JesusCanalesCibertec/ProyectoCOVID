using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoProcesoSeguimiento
    {
        public Int32 NumeroSolicitud { get; set; }
        public Int32 Secuencial { get; set; }
        public Int32 AprobadorId { get; set; }
        public String AprobadorNombre { get; set; }
        public String Comentario { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public String UnidadOperativaId { get; set; }
        public String UnidadOperativaNombre { get; set; }
    }
}
