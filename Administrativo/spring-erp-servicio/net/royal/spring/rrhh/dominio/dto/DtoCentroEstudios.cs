using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoCentroEstudios
    {
       public Nullable<Int32> idCentroEstudios{ get; set; }
       public String nombre{ get; set; }
       public String estadoId{ get; set; }
       public String estadoNombre{ get; set; }
    }
}
