using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoPermisosDetalleMarcas
    {
        public String fecha{ get; set; }
        public String hora{ get; set; }
        public String carnet{ get; set; }
        public Nullable<Int32> codigoempleado{ get; set; }
        public String nombre{ get; set; }
        public String estacion{ get; set; }
        public String tipo{ get; set; }
        public String observacion{ get; set; }
    }
}
