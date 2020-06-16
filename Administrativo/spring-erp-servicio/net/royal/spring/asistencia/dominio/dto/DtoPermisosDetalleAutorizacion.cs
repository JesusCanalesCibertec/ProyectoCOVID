using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoPermisosDetalleAutorizacion
    {
        public Nullable<DateTime> fecha{ get; set; }
        public Nullable<DateTime> fechafin{ get; set; }
        public String autorizadopor{ get; set; }
        public Nullable<DateTime> fechaautorizacion{ get; set; }
        public String observacion{ get; set; }
        public Nullable<Int32> numdias{ get; set; }
    }
}
