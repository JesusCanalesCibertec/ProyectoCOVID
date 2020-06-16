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
public class SeguridadautorizacionesPk {

	[Key]
	[Display(Name = "AplicacionCodigo")]
	[MaxLength(2)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("APLICACIONCODIGO")]
	public String Aplicacioncodigo  { get; set; }

	[Key]
	[Display(Name = "Grupo")]
	[MaxLength(6)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("GRUPO")]
	public String Grupo  { get; set; }

	[Key]
	[Display(Name = "Concepto")]
	[MaxLength(6)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("CONCEPTO")]
	public String Concepto  { get; set; }

	[Key]
	[Display(Name = "Usuario")]
	[MaxLength(20)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("USUARIO")]
	public String Usuario  { get; set; }
 }
}
