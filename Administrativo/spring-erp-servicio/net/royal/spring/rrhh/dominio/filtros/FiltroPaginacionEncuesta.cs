using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroPaginacionEncuesta
    {

        public String periodo { get; set; }
        public Nullable<Int32> secuencia { get; set; }
        public String titulo { get; set; }
        public String estado { get; set; }
        public String compania { get; set; }
        public String tipo { get; set; }
        public String programa { get; set; }
        public String Indicador { get; set; }
        public String Pregunta { get; set; }
        public String encuesta { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionEncuesta()
        {
            this.paginacion = new ParametroPaginacionGenerico();
        }

    }
}
