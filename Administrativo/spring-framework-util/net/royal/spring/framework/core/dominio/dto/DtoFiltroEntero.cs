using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoFiltroEntero : DominioOrden
    {
        public Int32? Codigo { get; set; }
        public String Nombre { get; set; }
        public String Estado { get; set; }
    }
}
