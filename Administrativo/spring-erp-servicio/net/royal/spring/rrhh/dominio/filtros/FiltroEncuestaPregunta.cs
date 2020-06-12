using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroEncuestaPregunta
    {
       public int? CodEncuesta { get; set; }
       public String DesEncuesta{ get; set; }
       public String TipoEncuesta { get; set; }
        public String AreaEncuesta{ get; set; }
       public String EstadoEncuesta { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroEncuestaPregunta()
        {
            paginacion = new ParametroPaginacionGenerico();
        }

    }
}
