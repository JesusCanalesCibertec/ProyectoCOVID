using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proyecto.dominio.filtro
{
    public class FiltroPaginacionProyecto
    {

        public FiltroPaginacionProyecto()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
        public String Nombre { get; set; }
        public String Estado { get; set; }
        public String IdProceso { get; set; }
        public String IdTipo { get; set; }
        public Int32? Codigo { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }
    }
}
