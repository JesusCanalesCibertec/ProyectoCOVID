using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.asistencia.dominio.filtro
{
    public class FiltrosSolicitudGeneral : DominioOrden
    {
        public Nullable<Int32> idSolicitante { get; set; }
        public String estado { get; set; }
        public Nullable<DateTime> fechaInicio { get; set; }
        public Nullable<DateTime> fechaFin { get; set; }

    }
}
