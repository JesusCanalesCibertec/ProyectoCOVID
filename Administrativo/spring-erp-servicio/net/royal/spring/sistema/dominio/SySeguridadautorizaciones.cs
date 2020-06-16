using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.sistema.dominio
{

/**
 * 
 * 
 * @tabla dbo.SY_SeguridadAutorizaciones
*/
[Table("SY_SEGURIDADAUTORIZACIONES")]
public class SySeguridadautorizaciones: SySeguridadautorizacionesPk {

	[Display(Name = "MasterBrowseFlag")]
	[MaxLength(1)]
	[Column("MASTERBROWSEFLAG")]
	public String Masterbrowseflag  { get; set; }

	[Display(Name = "MasterUpdateFlag")]
	[MaxLength(1)]
	[Column("MASTERUPDATEFLAG")]
	public String Masterupdateflag  { get; set; }

	[Display(Name = "MasterApproveFlag")]
	[MaxLength(1)]
	[Column("MASTERAPPROVEFLAG")]
	public String Masterapproveflag  { get; set; }

	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	[Display(Name = "Orden")]
	[Column("ORDEN")]
	public Nullable<Int32> Orden  { get; set; }

	public SySeguridadautorizaciones() {
	}
 }
}
