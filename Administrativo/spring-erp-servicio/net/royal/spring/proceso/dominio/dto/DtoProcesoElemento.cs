using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoProcesoElemento
    {
        public String idProceso { get; set; }
        public Int32 idVersion { get; set; }
        public String idElemento { get; set; }
    }
}
