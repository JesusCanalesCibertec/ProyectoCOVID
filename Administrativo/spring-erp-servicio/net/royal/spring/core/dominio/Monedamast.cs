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
 * @tabla dbo.MonedaMast
*/
[Table("MONEDAMAST")]
public class Monedamast: MonedamastPk {

	[Display(Name = "DescripcionCorta")]
	[MaxLength(20)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("DESCRIPCIONCORTA")]
	public String Descripcioncorta  { get; set; }

	[Display(Name = "Sigla")]
	[MaxLength(4)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("SIGLA")]
	public String Sigla  { get; set; }
        
	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }
        
	public Monedamast() {
	}
 }
}
