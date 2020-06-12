using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroPuestoempresa
    {
       public Int32? codigo { get; set; }
       public String nombre{ get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPuestoempresa()
        {
            paginacion = new ParametroPaginacionGenerico();
        }

    }
}
