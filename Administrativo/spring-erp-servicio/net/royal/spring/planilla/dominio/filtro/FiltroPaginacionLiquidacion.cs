using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class FiltroPaginacionLiquidacion
    {

        public String companiaSocio { get; set; }
        public Nullable<Int32> idEmpleado { get; set; }
        public String idTipoPlanilla { get; set; }
        public Nullable<DateTime> fechaLiquidacionInicio { get; set; }
        public Nullable<DateTime> fechaLiquidacionFin { get; set; }
        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionLiquidacion()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
