using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroPaginacionSujeto
    {

        public String companyowner { get; set; }
        public String periodo { get; set; }
        public int? secuencia { get; set; }
        public int? sujeto { get; set; }
        public DateTime? fecha { get; set; }
        public String institucion { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionSujeto()
        {
            this.paginacion = new ParametroPaginacionGenerico();
        }

    }
}
