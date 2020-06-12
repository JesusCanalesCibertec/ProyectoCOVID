using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.proceso.dominio.filtro
{
    public class FiltroRequerimiento
    {
        public FiltroRequerimiento()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
        public String IdRequerimiento { get; set; }
        public String Nombre { get; set; }
        
        public ParametroPaginacionGenerico paginacion { get; set; }
    }
}
