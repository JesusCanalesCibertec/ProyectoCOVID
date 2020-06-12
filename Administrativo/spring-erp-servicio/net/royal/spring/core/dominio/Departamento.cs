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
 * @tabla dbo.Departamento
*/
[Table("DEPARTAMENTO")]
public class Departamento: DepartamentoPk {

	[Display(Name = "DescripcionCorta")]
	[MaxLength(20)]
	[Column("DESCRIPCIONCORTA")]
	public String Descripcioncorta  { get; set; }
        
	[Display(Name = "Pais")]
	[MaxLength(3)]
	[Column("PAIS")]
	public String Pais  { get; set; }

	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }
        
	public Departamento() {
	}
 }
}
