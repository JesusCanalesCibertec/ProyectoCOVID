using net.royal.spring.programasocial.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoRendimientoEducativo
    {


        
        public String rendimiento { get; set; }
        public Nullable<Int32> cantLogMat { get; set; }
        public Nullable<Decimal> porcLogMat { get; set; }
        public Nullable<Int32> cantComuni { get; set; }
        public Nullable<Decimal> porcComuni { get; set; }
        public Nullable<Int32> cantComLec { get; set; }
        public Nullable<Decimal> porctComLec { get; set; }
        public Nullable<Int32> cantPerSoc { get; set; }
        public Nullable<Decimal> porcPerSoc { get; set; }
        public Nullable<Int32> cantCieAmb { get; set; }
        public Nullable<Decimal> porcCieAmb { get; set; }



    }
}
