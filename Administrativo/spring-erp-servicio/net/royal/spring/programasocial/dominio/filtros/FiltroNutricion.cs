using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroNutricion
    {
        public String Estado { get; set; }
        public String Periodo { get; set; }
        public int? CantidadRegistros { get; set; }
        public int? IdDiagnostico { get; set; }
        public String IdArea { get; set; }
        public String IdSexo { get; set; }
        public String TipoEdad { get; set; }
        public String NombreCompleto { get; set; }
        public String IdInstitucion { get; set; }
        public String IdPrograma { get; set; }
        public Boolean esNino { get; set; }
        public String IdValoracion { get; set; }

        public Int32? orden { get; set; }
        public Int32? sentido { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroNutricion()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
