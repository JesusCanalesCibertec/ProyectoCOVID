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
 * @tabla dbo.SY_UnidadReplicacion
*/
public class SyUnidadreplicacionPk {

	[Key]
	[Display(Name = "UnidadReplicacion")]
	[MaxLength(4)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("UNIDADREPLICACION")]
	public String Unidadreplicacion  { get; set; }
 }
}
