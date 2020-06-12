using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.salud.dominio.dto
{
    public class DtoDiagnostico
    {
        public string codigopadre{ get; set; }
        public string codigodiagnostico { get; set; }
        public String nombre{ get; set; }
        public String estado{ get; set; }
        public Nullable<DateTime> fechaReg { get; set; }

        public Nullable<Int32> idDiagnostico { get; set; }
        public String capitulo { get; set; }
        public String grupo { get; set; }


    }
}
