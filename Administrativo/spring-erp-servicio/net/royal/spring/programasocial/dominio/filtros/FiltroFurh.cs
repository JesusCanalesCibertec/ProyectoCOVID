using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroFurh
    {

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroFurh()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
