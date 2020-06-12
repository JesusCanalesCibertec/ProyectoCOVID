using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.proceso.dominio.filtro
{
    public class FiltroBpMaeproceso
    {
        public FiltroBpMaeproceso()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
        public String IdProceso { get; set; }
        public String Nombre { get; set; }
        
        public ParametroPaginacionGenerico paginacion { get; set; }
    }
}
