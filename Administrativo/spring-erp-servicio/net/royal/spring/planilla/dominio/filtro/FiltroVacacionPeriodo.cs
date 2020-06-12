using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.planilla.dominio.filtro
{
    public class FiltroVacacionPeriodo
    {
        public String companiaSocio { get; set; }
        public Nullable<Int32> idEmpleado { get; set; }
        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroVacacionPeriodo() {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
