using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.core.dominio
{

    /**
     * 
     * 
     * @tabla dbo.MA_MiscelaneosDetalle
*/
    [Table("MA_MISCELANEOSDETALLE")]
    public class MaMiscelaneosdetalle : MaMiscelaneosdetallePk
    {

        [Display(Name = "DescripcionLocal")]
        [MaxLength(250)]
        [Column("DESCRIPCIONLOCAL")]
        public String Descripcionlocal { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Display(Name = "VALORCODIGO1")]
        [MaxLength(4000)]
        [Column("VALORCODIGO1")]
        public String ValorCodigo1 { get; set; }

        [Display(Name = "VALORCODIGO2")]
        [MaxLength(4000)]
        [Column("VALORCODIGO2")]
        public String ValorCodigo2 { get; set; }

        [Display(Name = "VALORCODIGO4")]
        [MaxLength(4000)]
        [Column("VALORCODIGO4")]
        public String ValorCodigo4 { get; set; }

        [Column("VALORNUMERICO")]
        public Double? valorNumerico { get; set; }

        [Column("VALORNUMERICO2")]
        public Double? valorNumerico2 { get; set; }


        [Column("ORDEN")]
        public Nullable<Int32> Orden { get; set; }

        [NotMapped]
        public IList<Companyownerrecurso> listaRecursos { get; set; }

        [NotMapped]
        public int secuencia { get; set; }

        public MaMiscelaneosdetalle()
        {
        }
    }
}
