using net.royal.spring.programasocial.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoEstadoSalud
    {


        public String codInstitucion { get; set; }
        public String nomInstitucion { get; set; }
        public String codPrograma { get; set; }
        public Nullable<Decimal> sano { get; set; }
        public Nullable<Decimal> enfAguda { get; set; }
        public Nullable<Decimal> enfCronica{ get; set; }
        public Nullable<Decimal> discapacidad { get; set; }
        public Nullable<Decimal> sinEnf { get; set; }
        public Nullable<Decimal> discapacidadEnfAguda { get; set; }
        public Nullable<Decimal> discapacidadEnfCronica { get; set; }



    }
}
