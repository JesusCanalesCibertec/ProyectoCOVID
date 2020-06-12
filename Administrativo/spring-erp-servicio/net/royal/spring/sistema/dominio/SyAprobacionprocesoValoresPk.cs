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
 * @tabla dbo.SY_AprobacionProceso_Valores
*/
public class SyAprobacionprocesoValoresPk {

	[Key]
	[Display(Name = "CodigoProceso")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("CODIGOPROCESO")]
	public Nullable<Int32> Codigoproceso  { get; set; }

	[Key]
	[Display(Name = "Valor")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("VALOR")]
	public String Valor  { get; set; }
 }
}
