using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.core.dominio.filtro
{
    public class FiltroPaginacionAniversario
    {
        public Int32? IdPersona { get; set; }
        public String IdCentroCostos { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public Int32? Mes { get; set; }
        public Int32? Dia { get; set; }
        public Int32? Anios { get; set; }
        public String Signo { get; set; }

        public String compania { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionAniversario()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
