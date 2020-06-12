using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoBpMaeproceso
    {
        public String idProceso { get; set; }
        public String nombre { get; set; }
        public int? IdVersion { get; set; }
    }
}
