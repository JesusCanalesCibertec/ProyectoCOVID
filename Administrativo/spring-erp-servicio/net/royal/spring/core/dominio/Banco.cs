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
     * @tabla dbo.Banco
    */
    [Table("BANCO")]
    public class Banco: BancoPk {

	    [Display(Name = "DescripcionCorta")]
	    [MaxLength(20)]
	    [Column("DESCRIPCIONCORTA")]
	    public String Descripcioncorta  { get; set; }
        
	    [Display(Name = "Estado")]
	    [MaxLength(1)]
	    [Column("ESTADO")]
	    public String Estado  { get; set; }
        
	    public Banco() {
	    }
     }
}
