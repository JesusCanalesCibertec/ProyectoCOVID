using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.planilla.dominio.filtro
{
    public class FiltroPlanillaEmpleado
    {
        public String idCompaniaSocio{ get; set; }
        public Nullable<Int32> idEmpleado{ get; set; }
        public String idPeriodo{ get; set; }
        public String idTipoPlanilla{ get; set; }
        public String idTipoProceso{ get; set; }
    }
}
