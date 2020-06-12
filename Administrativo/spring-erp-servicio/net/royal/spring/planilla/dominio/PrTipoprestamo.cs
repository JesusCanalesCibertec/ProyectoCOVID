using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.planilla.dominio
{

    /**
     * 
     * 
     * @tabla dbo.PR_TipoPrestamo
    */
    [Table("PR_TIPOPRESTAMO")]
    public class PrTipoprestamo : PrTipoprestamoPk
    {

        [Display(Name = "Concepto")]
        [MaxLength(4)]
        [Column("CONCEPTO")]
        public String Concepto { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(40)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("MONEDAPRESTAMO")]
        public String Monedaprestamo { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }


        [Display(Name = "FlagWeb")]
        [MaxLength(1)]
        [Column("FlagWeb")]
        public String FlagVerWeb { get; set; }

        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public PrTipoprestamo()
        {
        }
    }
}
