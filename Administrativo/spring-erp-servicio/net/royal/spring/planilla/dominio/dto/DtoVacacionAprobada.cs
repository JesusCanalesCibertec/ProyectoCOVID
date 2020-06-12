using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.planilla.dominio.dto
{
    public class DtoVacacionAprobada
    {
        public Int32 numero { get; set; }
        public DateTime? fechaAprobacion { get; set; }
        public Int32 codigo { get; set; }
        public string colaborador { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public Int32 dias { get; set; }
        public String aprobador { get; set; }
    }
}
