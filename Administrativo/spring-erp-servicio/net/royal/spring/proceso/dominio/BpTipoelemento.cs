using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_TIPO_ELEMENTO", Schema = "sgseguridadsys")]
    public class BpTipoelemento : BpTipoelementoPk
    {
        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("FLG_ESTADO_INICIAL")]
        public String FlgEstadoinicial { get; set; }

        [Column("FLG_ESTADO_FINAL")]
        public String FlgEstadofinal { get; set; }

        [Column("ICONO_ESTILO")]
        public String IconoEstilo { get; set; }

        [Column("ICONO_RUTA")]
        public String IconoRuta { get; set; }

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

        public BpTipoelemento()
        {
        }
    }
}
