using net.royal.spring.programasocial.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoRetrasoEducativo
    {


        public String codInstitucion { get; set; }
        public String nomInstitucion { get; set; }
        public String tipoRetraso { get; set; }

        public Nullable<Decimal> regular { get; set; }
        public Nullable<Decimal> especial { get; set; }
        public Nullable<Decimal> regularInc { get; set; }
        public Nullable<Decimal> alternativa { get; set; }
        public Nullable<Decimal> cetpro { get; set; }
        public Nullable<Decimal> ninguna { get; set; }
        public Nullable<Decimal> total { get; set; }

        public Nullable<Decimal> regularP { get; set; }
        public Nullable<Decimal> especialP { get; set; }
        public Nullable<Decimal> regularIncP { get; set; }
        public Nullable<Decimal> alternativaP { get; set; }
        public Nullable<Decimal> cetproP { get; set; }
        public Nullable<Decimal> ningunaP { get; set; }
        public Nullable<Decimal> totalP { get; set; }


        public String tiempo { get; set; }
        public Nullable<Int32> cantidad { get; set; }
        public Nullable<Decimal> porcentaje { get; set; }






    }
}
