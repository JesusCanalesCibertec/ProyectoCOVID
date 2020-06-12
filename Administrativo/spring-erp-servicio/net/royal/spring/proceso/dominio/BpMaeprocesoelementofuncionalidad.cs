using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_MAE_PROCESO_ELEMENTO_FUNCIONALIDAD", Schema = "sgseguridadsys")]
    public class BpMaeprocesoelementofuncionalidad : BpMaeprocesoelementofuncionalidadPk
    {
        [Column("FLG_VISIBLE")]
        public String Visible { get; set; }

        [Column("FLG_HABILITADO")]
        public String Hablitado { get; set; }

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

        public BpMaeprocesoelementofuncionalidad()
        {

        }
        
    }
}
