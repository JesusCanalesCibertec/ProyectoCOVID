using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.salud.dominio.dto {
    public class FiltroDiagnostico {
        public String codigopadre { get; set; }
        public String codigodiagnostico { get; set; }
        public String nombre { get; set; }
        public String IdCapitulo { get; set; }
        public String IdGrupo { get; set; }
        public Nullable<Int32> estado { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroDiagnostico() {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
