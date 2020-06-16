using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroPosicionEmpresa: DtoFiltroEntero
    {
       public String Compania { get; set; }
       
    }
}
