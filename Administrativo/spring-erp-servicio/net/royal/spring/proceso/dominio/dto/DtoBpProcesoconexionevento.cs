using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoBpProcesoconexionevento
    {
        public String codProceso { get; set; }
        public Int32 codVersion { get; set; }
        public Int32 codConexion { get; set; }
        public String codEvento { get; set; }
        public String NomEvento { get; set; }
    }
}
