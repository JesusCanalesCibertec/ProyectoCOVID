using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoSyReporte
    {
        public String AplicacionCodigo { get; set; }
        public String AplicacionDescripcion { get; set; }
        public String ReporteCodigo { get; set; }
        public String Nombre { get; set; }
        public String TipoReporte { get; set; }
        public String Estado { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public DtoSyReporte()
        {
            this.paginacion = new ParametroPaginacionGenerico();
        }
    }
}
