using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoInstitucion
    {
        public String codigo{ get; set; }
        public String nombre{ get; set; }
        public Nullable<DateTime> fecha_registro{ get; set; }
        public String estado{ get; set; }
        

    }
}
