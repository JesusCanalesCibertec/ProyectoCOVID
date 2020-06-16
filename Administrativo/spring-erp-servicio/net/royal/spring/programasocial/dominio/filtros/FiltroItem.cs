using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroItem
    {
        public String codigo { get; set; }
        public String nombre { get; set; }
        public String estado { get; set; }
        public Int32? cantidad { get; set; }


        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroItem()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
