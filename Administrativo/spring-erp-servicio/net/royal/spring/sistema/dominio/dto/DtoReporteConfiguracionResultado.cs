using net.royal.spring.sistema.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoReporteConfiguracionResultado
    {
        public SyReporte SyReporte { get; set; }
        public Boolean FlgOk { get; set; }
        public String RutaCompletaReporte { get; set; }
        public byte[] Archivo { get; set; }

        public DtoReporteConfiguracionResultado() {
            FlgOk = false;
        }

    }
}
