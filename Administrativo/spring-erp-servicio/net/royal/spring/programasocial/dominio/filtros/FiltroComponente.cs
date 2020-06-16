using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroComponente
    {
       public String codigo { get; set; }
       public String nombre{ get; set; }
       public String estado{ get; set; }


        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroComponente()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
