using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoSyReporteArchivo
    {
        public String AplicacionCodigo { get; set; }
        public String ReporteCodigo { get; set; }
        public String Estado { get; set; }
        public String AplicacionDescripcion { get; set; }
        public String CompaniaSocio { get; set; }
        public String Periodo { get; set; }
        public String Version { get; set; }
        public byte[] Reporte { get; set; }
        public String Aux { get; set; }
        public String ReporteBase64 { get; set; }
    }
}
