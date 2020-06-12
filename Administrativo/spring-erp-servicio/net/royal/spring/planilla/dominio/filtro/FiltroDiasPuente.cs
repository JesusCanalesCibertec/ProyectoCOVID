using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class FiltroDiasPuente
    {
        public String fechamesdia { get; set; }
        public String descripcion { get; set; }
        public String estado { get; set; }
        public Nullable<DateTime> desde { get; set; }
        public Nullable<DateTime> hasta { get; set; }
    }
}
