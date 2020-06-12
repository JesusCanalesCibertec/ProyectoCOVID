using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroPaginacionCapacitacion
    {

        public Nullable<Int32> idSolicitante { get; set; }
        public String estado { get; set; }
        public Nullable<DateTime> fechaInicio { get; set; }
        public Nullable<DateTime> fechaFin { get; set; }

        public Nullable<Int32> idcurso { get; set; }
        public String institucion { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionCapacitacion()
        {
            this.paginacion = new ParametroPaginacionGenerico();
        }

    }
}
