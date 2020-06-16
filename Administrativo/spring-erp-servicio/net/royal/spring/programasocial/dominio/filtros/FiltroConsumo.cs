using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroConsumo
    {
       public String codigoInstitucion { get; set; }
       public String nombreInstitucion{ get; set; }

        public String codigoPeriodo { get; set; }

        public Nullable<DateTime> fechainicial { get; set; }

        public Nullable<DateTime> fechafinal { get; set; }

        public String estado { get; set; }




        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroConsumo()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
