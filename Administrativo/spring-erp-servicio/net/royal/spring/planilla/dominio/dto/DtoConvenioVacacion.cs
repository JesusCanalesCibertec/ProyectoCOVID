using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoConvenioVacacion
    {
     
        public Nullable<Int32> empleadoId{ get; set; }
        public String empleadoNombre{ get; set; }
      
        public String empleadoDocumento{ get; set; }
        public String empleadoDireccion{ get; set; }
        public String puestoNombre { get; set; }
        public Nullable<DateTime> fechaIngreso { get; set; }
        public String fecha { get; set; }
        public String periodo { get; set; }

    }
}
