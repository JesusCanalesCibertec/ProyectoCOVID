using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroCentroEstudio: DominioOrden
    {
       public int? idCentroEstudios { get; set; }
       public String nombre { get; set; }
       public String estado { get; set; }
    }
}
