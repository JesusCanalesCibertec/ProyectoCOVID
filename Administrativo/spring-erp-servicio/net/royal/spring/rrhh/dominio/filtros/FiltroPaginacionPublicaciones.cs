using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroPaginacionPublicaciones
    {
        public String publicacion { get; set; }
        public String aplicacion { get; set; }
        public String estado { get; set; }
        public Nullable<DateTime> desde { get; set; }
        public Nullable<DateTime> hasta { get; set; }
        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionPublicaciones()
        {
            this.paginacion = new ParametroPaginacionGenerico();
        }
    }
}
