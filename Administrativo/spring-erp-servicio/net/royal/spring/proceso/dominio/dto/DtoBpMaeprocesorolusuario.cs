using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoBpMaeprocesorolusuario
    {
        public String codProceso { get; set; }
        public String codRol { get; set; }
        public String codTipoSeguridad { get; set; }
        public String codConcepto { get; set; }
        public String NomConcepto { get; set; }
    }
}
