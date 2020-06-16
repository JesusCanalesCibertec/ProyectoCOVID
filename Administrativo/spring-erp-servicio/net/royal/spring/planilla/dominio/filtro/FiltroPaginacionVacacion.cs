using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class FiltroPaginacionVacacion
    {

        public Nullable<Int32> idEmpleado{ get; set; }
        public Nullable<Int32> idSolicitante{ get; set; }
        public String idUtilizacion{ get; set; }
        public String estado{ get; set; }
        public Nullable<DateTime> fechaInicio{ get; set; }
        public Nullable<DateTime> fechaFin{ get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionVacacion() {
            ParametroPaginacionGenerico paginacion = new ParametroPaginacionGenerico();
        }
    }
}
