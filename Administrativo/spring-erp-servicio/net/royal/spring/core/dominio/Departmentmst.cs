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
 * @tabla dbo.departmentmst
*/
[Table("DEPARTMENTMST")]
public class Departmentmst: DepartmentmstPk {

	[Display(Name = "description")]
	[MaxLength(30)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("DESCRIPTION")]
	public String Description  { get; set; }
        
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

	public Departmentmst() {
	}
 }
}
