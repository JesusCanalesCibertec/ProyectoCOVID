using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoBpMaeprocesoelementofuncionalidad
    {
        public String codProceso { get; set; }
        public String codElemento { get; set; }
        public String codFuncionalidad { get; set; }
        public String nomFuncionalidad { get; set; }
        public String flagVisible { get; set; }
        public String flagHabilitado { get; set; }
    }
}
