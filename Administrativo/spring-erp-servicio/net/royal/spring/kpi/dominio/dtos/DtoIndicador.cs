using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoIndicador
    {
        public String codigo { get; set; }
        public String nombre { get; set; }
        public string nom_programa { get; set; }
        public string nom_componente { get; set; }
        public String estado { get; set; }
        public String estado2 { get; set; }


        public String Comentario { get; set; }
        public Nullable<Decimal> Porcentaje { get; set; }


    }
}
