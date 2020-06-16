using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroPaginacionRequerimiento
    {
       public Nullable<Int32> IdSolicitante { get; set; }
       public String Estado { get; set; }
       public Nullable<DateTime> FechaInicio { get; set; }
       public Nullable<DateTime> FechaFin { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionRequerimiento()
        {
            this.paginacion = new ParametroPaginacionGenerico();
        }
    }
}
