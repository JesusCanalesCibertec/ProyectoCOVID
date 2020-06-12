using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.contabilidad.dominio.filtro
{
    public class FiltroAcCostcentermst : DominioOrden
    {
        public String Codigo { get; set; }
        public String Nombre { get; set; }
        public String Estado { get; set; }
    }
}
