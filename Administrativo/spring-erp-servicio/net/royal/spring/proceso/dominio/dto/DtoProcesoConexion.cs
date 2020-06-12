using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoProcesoConexion
    {
        public String idProceso { get; set; }
        public Int32 idVersion { get; set; }
        public Int32 idConexion { get; set; }

        public String accion { get; set; }
        public String elementoEntrada { get; set; }
        public String elementoSalida { get; set; }
    }
}
