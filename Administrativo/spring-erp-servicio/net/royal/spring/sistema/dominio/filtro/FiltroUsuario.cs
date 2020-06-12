using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroUsuario
    {
       public string codUsuario { get; set; }
       public String nombre{ get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroUsuario()
        {
            paginacion = new ParametroPaginacionGenerico();
        }

    }
}
