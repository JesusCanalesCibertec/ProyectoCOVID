using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class FiltroPaginacionCtsPorEmpleado
    {
        public Nullable<Int32> idEmpleado { get; set; }
        public Nullable<Int32> numeroCts { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionCtsPorEmpleado() {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
