using net.royal.spring.programasocial.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoEstadoFuncional
    {


        public String nomInstitucion { get; set; }
        public Nullable<Int32> poblacion { get; set; }
        public Nullable<Int32> cantInde { get; set; }
        public Nullable<Decimal> porcInde { get; set; }
        public Nullable<Int32> cantDepeLeve { get; set; }
        public Nullable<Decimal> porcDepeLeve { get; set; }
        public Nullable<Int32> cantDepeMode { get; set; }
        public Nullable<Decimal> porcDepeMode { get; set; }
        public Nullable<Int32> cantDepeGrave { get; set; }
        public Nullable<Decimal> porcDepeGrave { get; set; }
        public Nullable<Int32> cantEnfMental { get; set; }
        public Nullable<Decimal> porcEnfMental { get; set; }
        public Nullable<Int32> cantTotal { get; set; }
        public Nullable<Decimal> porcTotal { get; set; }

        


    }
}
