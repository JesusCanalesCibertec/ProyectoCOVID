using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoBpMaeprocesoestado
    {
        public String idProceso { get; set; }
        public String nomProceso { get; set; }
        public String idEstado { get; set; }
        public String nomEstado { get; set; }
        public String porDefecto { get; set; }
    }
}
