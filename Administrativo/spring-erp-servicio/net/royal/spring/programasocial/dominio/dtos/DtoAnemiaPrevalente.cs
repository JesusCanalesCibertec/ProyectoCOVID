using net.royal.spring.programasocial.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoAnemiaPrevalente
    {


        public String codInstitucion { get; set; }
        public String nomInstitucion { get; set; }
        public Nullable<Decimal> mascPTotal { get; set; }
        public Nullable<Decimal> femePTotal { get; set; }
        public Nullable<Decimal> mascSinAnemia { get; set; }
        public Nullable<Decimal> femeSinAnemia { get; set; }
        public Nullable<Decimal> mascAnemiaLeve { get; set; }
        public Nullable<Decimal> femeAnemiaLeve { get; set; }
        public Nullable<Decimal> mascAnemiaMod { get; set; }
        public Nullable<Decimal> femeAnemiaMod { get; set; }
        public Nullable<Decimal> mascAnemiaSev { get; set; }
        public Nullable<Decimal> femeAnemiaSev { get; set; }
        public Nullable<Decimal> mascNoEvaluado { get; set; }
        public Nullable<Decimal> femeNoEvaluado { get; set; }
        


    }
}
