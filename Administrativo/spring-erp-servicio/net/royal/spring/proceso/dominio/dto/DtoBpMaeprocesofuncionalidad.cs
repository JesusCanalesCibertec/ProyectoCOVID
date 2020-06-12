using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoBpMaeprocesofuncionalidad
    {
        public String idProceso { get; set; }
        public String nomProceso { get; set; }
        public String idFuncionalidad { get; set; }
        public String nomFuncionalidad { get; set; }
        public String visibleDefecto { get; set; }
        public String habilitadoDefecto { get; set; }
    }
}
