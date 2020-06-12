using net.royal.spring.programasocial.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoAccesoEducacion
    {


        public String codInstitucion { get; set; }
        public String nomInstitucion { get; set; }
        public Nullable<Int32> cantFemeRegular { get; set; }
        public Nullable<Int32> cantMascRegular { get; set; }
        public Nullable<Decimal> porcFemeRegular { get; set; }
        public Nullable<Decimal> porcMascRegular { get; set; }
        public Nullable<Int32> cantFemeEspecial { get; set; }
        public Nullable<Int32> cantMascEspecial { get; set; }
        public Nullable<Decimal> porcFemeEspecial { get; set; }
        public Nullable<Decimal> porcMascEspecial { get; set; }
        public Nullable<Int32> cantFemeRegularInc { get; set; }
        public Nullable<Int32> cantMascRegularInc { get; set; }
        public Nullable<Decimal> porcFemeRegularInc { get; set; }
        public Nullable<Decimal> porcMascRegularInc { get; set; }
        public Nullable<Int32> cantFemeAlternativa { get; set; }
        public Nullable<Int32> cantMascAlternativa { get; set; }
        public Nullable<Decimal> porcFemeAlternativa { get; set; }
        public Nullable<Decimal> porcMascAlternativa { get; set; }
        public Nullable<Int32> cantFemeCEPTRO { get; set; }
        public Nullable<Int32> cantMascCEPTRO { get; set; }
        public Nullable<Decimal> porcFemeCEPTRO { get; set; }
        public Nullable<Decimal> porcMascCEPTRO { get; set; }
        public Nullable<Int32> cantFemeNinguno { get; set; }
        public Nullable<Int32> cantMascNinguno { get; set; }
        public Nullable<Decimal> porcFemeNinguno { get; set; }
        public Nullable<Decimal> porcMascNinguno { get; set; }
        public Nullable<Int32> cantFemeTotal { get; set; }
        public Nullable<Int32> cantMascTotal { get; set; }
        public Nullable<Decimal> porcFemeTotal { get; set; }
        public Nullable<Decimal> porcMascTotal { get; set; }


    }
}
