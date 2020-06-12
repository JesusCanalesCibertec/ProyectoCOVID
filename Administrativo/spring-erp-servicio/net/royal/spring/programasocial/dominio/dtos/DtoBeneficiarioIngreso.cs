using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoBeneficiarioIngreso
    {
        public Nullable<Int32> idIngreso { get; set; }
        public Nullable<DateTime> fechaingreso{ get; set; }
        public String situacionlegal { get; set; }
        public String comentariosingreso { get; set; }
        public Nullable<DateTime> fechaegreso { get; set; }
        public String motivoegreso { get; set; }
        public String comentariosegreso { get; set; }

    }
}
