using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.core.dominio.filtro
{
    public class FiltroPaginacionCumpleanio
    {


        public Int32? IdPersona { get; set; }
        public String IdCentroCostos { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string compania { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionCumpleanio()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
