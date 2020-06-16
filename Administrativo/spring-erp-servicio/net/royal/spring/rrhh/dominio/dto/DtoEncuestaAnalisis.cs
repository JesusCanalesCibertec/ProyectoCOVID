using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoEncuestaAnalisis
    {
        public Nullable<Int32> cantidad { get; set; }
        public String area { get; set; }
        public String pregunta { get; set; }
        public String respuesta { get; set; }

        public Nullable<Decimal> peso { get; set; }
        public Nullable<Decimal> puntaje { get; set; }
        public Nullable<Decimal> maximo { get; set; }
    }
}
