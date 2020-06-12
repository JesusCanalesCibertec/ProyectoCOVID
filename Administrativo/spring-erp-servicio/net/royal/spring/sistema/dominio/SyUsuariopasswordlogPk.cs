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
 * @tabla dbo.SY_UsuarioPasswordLog
*/
public class SyUsuariopasswordlogPk {

	[Key]
	[Display(Name = "Usuario")]
	[MaxLength(20)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("USUARIO")]
	public String Usuario  { get; set; }

	[Key]
	[Display(Name = "Secuencia")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("SECUENCIA")]
	public Nullable<Int32> Secuencia  { get; set; }
 }
}
