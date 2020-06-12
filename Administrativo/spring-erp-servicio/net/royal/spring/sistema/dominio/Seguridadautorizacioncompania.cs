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
 * @tabla dbo.SeguridadAutorizacionCompania
*/
[Table("SEGURIDADAUTORIZACIONCOMPANIA")]
public class Seguridadautorizacioncompania: SeguridadautorizacioncompaniaPk {

	[Display(Name = "Estado")]
	[MaxLength(2)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	public Seguridadautorizacioncompania() {
	}
 }
}
