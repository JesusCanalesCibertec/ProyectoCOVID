using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoBpProcesorequerimiento
    {
        public Int32? idVersion { get; set; }
        public String idProceso { get; set; }
        public String nomProceso { get; set; }
        public String idRequerimiento { get; set; }
        public String nomRequerimiento { get; set; }
        public String obligatorio { get; set; }
    }
}
