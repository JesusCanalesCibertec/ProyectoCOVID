using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.rrhh.dominio
{

/**
 * 
 * 
 * @tabla dbo.HR_UNIDADOPERATIVA
*/
[Table("HR_UNIDADOPERATIVA")]
public class HrUnidadoperativa: HrUnidadoperativaPk {

	[Display(Name = "DESCRIPCION")]
	[MaxLength(500)]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

	[Display(Name = "RESPONSABLE")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("RESPONSABLE")]
	public Nullable<Int32> Responsable  { get; set; }

	[Display(Name = "ESTADO")]
	[MaxLength(1)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	[Display(Name = "UNIDADOPERATIVA_SUPERIOR")]
	[MaxLength(4)]
	[Column("UNIDADOPERATIVA_SUPERIOR")]
	public String UnidadoperativaSuperior  { get; set; }

	public HrUnidadoperativa() {
	}
 }
}
