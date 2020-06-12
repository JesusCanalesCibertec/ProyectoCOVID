using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroBeneficiario
    {
        public Nullable<Int32> codigo { get; set; }
        public String nombre { get; set; }
        public String dni { get; set; }
        public String institucion { get; set; }
        public Nullable<DateTime> desdeNac { get; set; }
        public Nullable<DateTime> hastaNac { get; set; }
        public Nullable<DateTime> desdeReg { get; set; }
        public Nullable<DateTime> hastaReg { get; set; }
        public String sexo { get; set; }
        public String programa { get; set; }
        public String estado { get; set; }
        public Nullable<Int32> edad { get; set; }
        public String area { get; set; }

        public Int32 orden { get; set; }
        public Int32 sentido { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroBeneficiario()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
