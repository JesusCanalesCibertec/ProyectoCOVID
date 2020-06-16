using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoItem
    {
        public String idItem { get; set; }
        public String nomItem { get; set; }
        public String nomTipo { get; set; }
        public String nomClase { get; set; }
        public String nomGrupo { get; set; }
        public String estado { get; set; }
        public String nomUnidad { get; set; }
        public Decimal? coeficiente { get; set; }

    }
}
