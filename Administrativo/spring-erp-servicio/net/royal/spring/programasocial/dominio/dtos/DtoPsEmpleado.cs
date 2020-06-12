using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoPsEmpleado
    {
        public String idInstitucion { get; set; }
        public Nullable<Int32> idEmpleado{ get; set; }
        public String nombre{ get; set; }
        public String documento{ get; set; }
        public String auxInstitucion { get; set; }
        public String sexo { get; set; }
        public Int32 edad { get; set; }
    }
}
