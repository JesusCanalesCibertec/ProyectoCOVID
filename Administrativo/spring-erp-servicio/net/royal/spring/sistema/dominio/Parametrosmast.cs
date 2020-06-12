using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.sistema.dominio
{

    /**
     * 
     * 
     * @tabla dbo.ParametrosMast
*/
    [Table("PARAMETROSMAST")]
    public class Parametrosmast : ParametrosmastPk
    {

        [Display(Name = "DescripcionParametro")]
        [MaxLength(70)]
        [Column("DESCRIPCIONPARAMETRO")]
        public String Descripcionparametro { get; set; }

        [Display(Name = "Explicacion")]
        [MaxLength(150)]
        [Column("EXPLICACION")]
        public String Explicacion { get; set; }

        [Display(Name = "TipodeDatoFlag")]
        [MaxLength(1)]
        [Column("TIPODEDATOFLAG")]
        public String Tipodedatoflag { get; set; }

        [Display(Name = "Texto")]
        [MaxLength(20)]
        [Column("TEXTO")]
        public String Texto { get; set; }

        [Display(Name = "Numero")]
        [Column("NUMERO")]
        public Nullable<Single> Numero { get; set; }

        [Display(Name = "Fecha")]
        [Column("FECHA")]
        public Nullable<DateTime> Fecha { get; set; }

        [Display(Name = "FinanceComunFlag")]
        [MaxLength(1)]
        [Column("FINANCECOMUNFLAG")]
        public String Financecomunflag { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        public Parametrosmast()
        {
        }
    }
}
