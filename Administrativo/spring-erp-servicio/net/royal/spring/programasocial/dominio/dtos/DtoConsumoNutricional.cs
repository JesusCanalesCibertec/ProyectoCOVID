using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoConsumoNutricional
    {

        public String codItem{ get; set; }
        public String nomItem{ get; set; }
        public String nomUnidad { get; set; }
        public Nullable<Decimal> cantidadsolicitada { get; set; }
        public Nullable<Decimal> cantidadfundacion { get; set; }
        public Nullable<Decimal> cantidadotros { get; set; }
        public String comentarios { get; set; }
        public String grupo { get; set; }


    }
}
