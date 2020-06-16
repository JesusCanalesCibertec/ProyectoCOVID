using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoEncuestapregunta
    {
        public Nullable<Int32> pregunta{ get; set; }
        public String descripcion{ get; set; }
        public String tipoencuestaId{ get; set; }
        public String tipoencuestaNombre{ get; set; }
        public String areaId{ get; set; }
        public String areaNombre{ get; set; }
        public String estado{ get; set; }

    }
}
