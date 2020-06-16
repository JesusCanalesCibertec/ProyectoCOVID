using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.planilla.dominio
{
    [Table("Pr_Calendarioferiados")]
    public class PrCalendarioferiados : PrCalendarioferiadosPk
    {
        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        public PrCalendarioferiados()
        {
        }
    }
}
