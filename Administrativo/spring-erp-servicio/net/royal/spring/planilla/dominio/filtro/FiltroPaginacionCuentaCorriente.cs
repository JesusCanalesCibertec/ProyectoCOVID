using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class FiltroPaginacionCuentaCorriente
    {
        public Int32 idEmpleado { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }
        public FiltroPaginacionCuentaCorriente() {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
