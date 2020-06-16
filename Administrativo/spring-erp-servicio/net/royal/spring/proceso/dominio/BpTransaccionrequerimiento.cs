using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.BP_TRANSACCION_SEGUIMIENTO
*/
    [Table("BP_TRANSACCION_REQUERIMIENTO", Schema = "sgseguridadsys")]
    public class BpTransaccionrequerimiento : BpTransaccionrequerimientoPk
    {
        [Column("FLG_CUMPLIDO")]
        public String FlagCumplido { get; set; }

        [Column("FLG_OBLIGATORIO")]
        public String FlagObligatorio { get; set; }

        [Column("CREACION_USUARIO")]
        public String CreacionUsuario { get; set; }

        [Column("CREACION_FECHA")]
        public Nullable<DateTime> CreacionFecha { get; set; }

        [Column("CREACION_TERMINAL")]
        public String CreacionTerminal { get; set; }

        [Column("MODIFICACION_USUARIO")]
        public String ModificacionUsuario { get; set; }

        [Column("MODIFICACION_FECHA")]
        public Nullable<DateTime> ModificacionFecha { get; set; }

        [Column("MODIFICACION_TERMINAL")]
        public String ModificacionTerminal { get; set; }

        public BpTransaccionrequerimiento()
        {
        }
    }
}
