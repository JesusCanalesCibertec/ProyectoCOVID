using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.programasocial.dominio.dtos {
    public class DtoGraficoMultiple {
        public String name { get; set; }
        public List<Series> series { get; set; }
    }


    public class Series {
        public String name { get; set; }
        public Nullable<Decimal> value { get; set; }
    }
}
