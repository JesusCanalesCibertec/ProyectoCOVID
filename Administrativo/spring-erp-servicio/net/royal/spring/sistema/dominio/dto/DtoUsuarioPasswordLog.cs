using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto
{
   public class DtoUsuarioPasswordLog
    {
        public DateTime FechaUltimoLogin { get; set; }
        public int? NumeroLogins { get; set; }
    }
}
