using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class FiltroPaginacionPrestamo
    {
        public String perfil { get; set; }
        public String estado { get; set; }
        public String tipoPrestamo { get; set; }
        public Int32 idEmpleado { get; set; }
        public Nullable<DateTime> fechaInicio { get; set; }
        public Nullable<DateTime> fechaFin { get; set; }
        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionPrestamo()
        {
            this.paginacion = new ParametroPaginacionGenerico();
        }
    }
}
