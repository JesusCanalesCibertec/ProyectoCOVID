using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoTransaccionNotificacion
    {
        public String descripcion { get; set; }
        public Int32 idTransaccion { get; set; }
        public String componente { get; set; }
        public String idProceso { get; set; }
        public String nombreCorto { get; set; }
    }
}
