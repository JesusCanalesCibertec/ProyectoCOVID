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
 * @tabla dbo.accountmst
*/
[Table("ACCOUNTMST")]
public class Accountmst: AccountmstPk {

	[Display(Name = "prime")]
	[MaxLength(10)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PRIME")]
	public String Prime  { get; set; }

	[Display(Name = "element")]
	[MaxLength(6)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ELEMENT")]
	public String Element  { get; set; }

	[Display(Name = "localname")]
	[MaxLength(100)]
	[Column("LOCALNAME")]
	public String Localname  { get; set; }
        
	[Display(Name = "status")]
	[MaxLength(1)]
	[Column("STATUS")]
	public String Status  { get; set; }

	[Display(Name = "lastuser")]
	[MaxLength(10)]
	[Column("LASTUSER")]
	public String Lastuser  { get; set; }

	[Display(Name = "lastdate")]
	[Column("LASTDATE")]
	public Nullable<DateTime> Lastdate  { get; set; }
        
	public Accountmst() {
	}
 }
}
