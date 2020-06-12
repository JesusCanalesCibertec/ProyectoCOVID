using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.planilla.dominio.filtro
{
    public class FiltroVacacionUtilizacion : DominioOrden
    {
        public String companiaSocio { get; set; }
        public Int32 idEmpleado { get; set; }
        public Nullable<Int32> numeroPeriodo { get; set; }
    }
}
