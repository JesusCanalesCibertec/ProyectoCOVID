using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroArbolOrganigrama
    {
        public String CompaniaSocio { get; set; }
        public int Anio { get; set; }
        public int Secuencia { get; set; }
        public String Descripcion { get; set; }
        public string orden { get; set; }
        public string Codigo { get; set; }
        public UsuarioActual usuario { get; set; }
        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroArbolOrganigrama()
        {
            this.paginacion = new ParametroPaginacionGenerico();
        }
    }
}
