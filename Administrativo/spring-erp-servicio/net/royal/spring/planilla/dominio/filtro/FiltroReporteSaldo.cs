using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.planilla.dominio.filtro
{
    public class FiltroReporteSaldo
    {
        public int? personaId { get; set; }
        public int? jefe { get; set; }
        public bool masivo { get; set; }
        public bool detalle { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroReporteSaldo() {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
