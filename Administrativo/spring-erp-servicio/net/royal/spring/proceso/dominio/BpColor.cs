using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("ColorMast")]
    public class BpColor : BpColorPk
    {
        [Column("X")]
        public String Nombre { get; set; }

        [Column("DescripcionExtranjera")]
        public String Descripcion { get; set; }

        [Column("Estado")]
        public String Estado { get; set; }

        [Column("UltimoUsuario")]
        public String UltimoUsuario { get; set; }

        [Column("UltimaFechaModif")]
        public Nullable<DateTime> UltimaFechaModif { get; set; }

        [Column("DescripcionCorta")]
        public String DescripcionCorta { get; set; }

        public BpColor()
        {
        }
    }
}
