using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroPaginacionCurso: DominioOrden
    {
       public int? idCurso { get; set; }
        public String nombre { get; set; }
        public String estado { get; set; }
        public String idArea { get; set; }
        public String idTipo { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionCurso()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
