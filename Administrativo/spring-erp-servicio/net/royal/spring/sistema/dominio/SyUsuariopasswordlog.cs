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
[Table("SY_USUARIOPASSWORDLOG")]
public class SyUsuariopasswordlog: SyUsuariopasswordlogPk {

	[Display(Name = "Clave")]
	[MaxLength(100)]
	[Column("CLAVE")]
	public String Clave  { get; set; }

	public SyUsuariopasswordlog() {
	}
 }
}
