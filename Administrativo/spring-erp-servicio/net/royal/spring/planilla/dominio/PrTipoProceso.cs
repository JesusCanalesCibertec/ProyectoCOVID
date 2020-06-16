using net.royal.spring.planilla.dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.planilla.dominio
{
    [Table("PR_TIPOPROCESO")]
    public class PrTipoProceso : PrTipoProcesoPk
    {
        [Display(Name = "Descripcion")]
        [MaxLength(50)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }


        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("Estado")]
        public String Estado { get; set; }
    }
}
