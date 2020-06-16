using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroMiscelaneosheader
    {
       public String aplicacion{ get; set; }
       public String codigo { get; set; }
        public String descripcion{ get; set; }
       public String estado { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroMiscelaneosheader()
        {
            paginacion = new ParametroPaginacionGenerico();
        }

    }
}
