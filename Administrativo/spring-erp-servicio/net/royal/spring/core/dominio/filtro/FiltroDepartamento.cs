using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.core.dominio.filtro
{
    public class FiltroDepartamento : DominioOrden
    {
        public String IdPais { get; set; }
        public String Codigo { get; set; }
        public String Nombre { get; set; }
        public String Estado { get; set; }
    }
}
