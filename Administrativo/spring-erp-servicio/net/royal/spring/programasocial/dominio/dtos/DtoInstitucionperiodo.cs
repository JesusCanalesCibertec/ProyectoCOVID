using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto {
    public class DtoInstitucionperiodo {
        public String codInstitucion { get; set; }
        public String codAplicacion { get; set; }
        public String codGrupo { get; set; }
        public String codConcepto { get; set; }
        public String codPeriodo { get; set; }
        public String codPeriodoCopiar { get; set; }
        public Nullable<DateTime> fechainicio { get; set; }
        public Nullable<DateTime> fechafin { get; set; }
        public Nullable<DateTime> fechainicioregistro { get; set; }
        public Nullable<DateTime> fechafinregistro { get; set; }
        public String nomConcepto { get; set; }

    }
}
