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
 * @tabla dbo.SeguridadAutorizaciones
*/
[Table("SEGURIDADAUTORIZACIONES")]
public class Seguridadautorizaciones: SeguridadautorizacionesPk {

	[Display(Name = "OpcionAgregarFlag")]
	[MaxLength(1)]
	[Column("OPCIONAGREGARFLAG")]
	public String Opcionagregarflag  { get; set; }

	[Display(Name = "OpcionModificarFlag")]
	[MaxLength(1)]
	[Column("OPCIONMODIFICARFLAG")]
	public String Opcionmodificarflag  { get; set; }

	[Display(Name = "OpcionBorrarFlag")]
	[MaxLength(1)]
	[Column("OPCIONBORRARFLAG")]
	public String Opcionborrarflag  { get; set; }

	[Display(Name = "OpcionAprobarFlag")]
	[MaxLength(1)]
	[Column("OPCIONAPROBARFLAG")]
	public String Opcionaprobarflag  { get; set; }

	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	public Seguridadautorizaciones() {
	}
 }
}
