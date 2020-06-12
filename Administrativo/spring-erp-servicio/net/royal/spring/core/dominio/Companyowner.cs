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
 * @tabla dbo.companyowner
*/
[Table("COMPANYOWNER")]
public class Companyowner: CompanyownerPk {

	[Display(Name = "description")]
	[MaxLength(40)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("DESCRIPTION")]
	public String Description  { get; set; }
        
	[Display(Name = "company")]
	[MaxLength(6)]
	[Column("COMPANY")]
	public String Company  { get; set; }

	[Display(Name = "owner")]
	[MaxLength(2)]
	[Column("OWNER")]
	public String Owner  { get; set; }
        
	[Display(Name = "lastuser")]
	[MaxLength(10)]
	[Column("LASTUSER")]
	public String Lastuser  { get; set; }

	[Display(Name = "lastdate")]
	[Column("LASTDATE")]
	public Nullable<DateTime> Lastdate  { get; set; }

	public Companyowner() {
	}
 }
}
