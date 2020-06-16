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
 * @tabla dbo.afemst
*/
[Table("AFEMST")]
public class Afemst: AfemstPk {

	[Display(Name = "companyowner")]
	[MaxLength(8)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("COMPANYOWNER")]
	public String Companyowner  { get; set; }

	[Display(Name = "localname")]
	[MaxLength(40)]
	[Column("LOCALNAME")]
	public String Localname  { get; set; }
        
	[Display(Name = "afetype")]
	[MaxLength(2)]
	[Column("AFETYPE")]
	public String Afetype  { get; set; }
        
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
        

	public Afemst() {
	}
 }
}
