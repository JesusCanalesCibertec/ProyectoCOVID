using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoConsumoPlantilla
    {
        public String codInstitucion { get; set; }
        public String codItem { get; set; }

        public Nullable<Int32>  plantilla { get; set; }
        public String nomItem{ get; set; }
        public String nomUnidad { get; set; }
     

    }
}
