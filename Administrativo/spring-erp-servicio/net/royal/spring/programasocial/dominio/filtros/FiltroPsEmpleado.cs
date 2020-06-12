using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroPsEmpleado
    {
        public String codInstitucion { get; set; }
        public Int32? codEmpleado { get; set; }
        public String nomEmpleado { get; set; }
        public String numDocumento { get; set; }
        public Nullable<Int32> edad { get; set; }
        public String area { get; set; }
        public String sexo { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPsEmpleado()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
