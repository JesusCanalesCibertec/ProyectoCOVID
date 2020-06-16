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
 * @tabla dbo.SeguridadAutorizacionReporte
*/
[Table("SEGURIDADAUTORIZACIONREPORTE")]
public class Seguridadautorizacionreporte: SeguridadautorizacionreportePk {

	[Display(Name = "Disponible")]
	[MaxLength(1)]
	[Column("DISPONIBLE")]
	public String Disponible  { get; set; }


	public Seguridadautorizacionreporte() {
	}
 }
}
