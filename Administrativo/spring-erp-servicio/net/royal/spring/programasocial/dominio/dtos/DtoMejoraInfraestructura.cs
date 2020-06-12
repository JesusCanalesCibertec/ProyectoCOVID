using net.royal.spring.programasocial.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoMejoraInfraestructura
    {


        public String nomInstitucion { get; set; }
        public String nomComponente { get; set; }
        public String aspecto { get; set; }
        public String funcBueno { get; set; }
        public String funcRegu { get; set; }
        public String funcMalo { get; set; }
        public String estrBueno { get; set; }
        public String estrRegu { get; set; }
        public String estrMalo { get; set; }

        public String estrBueno2 { get; set; }
        public String estrRegu2 { get; set; }
        public String estrMalo2 { get; set; }

        public Int32 cantidad { get; set; }



    }
}
