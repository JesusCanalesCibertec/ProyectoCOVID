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
[Table("SY_UNIDADREPLICACION")]
public class SyUnidadreplicacion: SyUnidadreplicacionPk {

	[Display(Name = "DescripcionLocal")]
	[MaxLength(30)]
	[Column("DESCRIPCIONLOCAL")]
	public String Descripcionlocal  { get; set; }

	[Display(Name = "DescripcionExtranjera")]
	[MaxLength(30)]
	[Column("DESCRIPCIONEXTRANJERA")]
	public String Descripcionextranjera  { get; set; }

	[Display(Name = "RangoPersonaInicio")]
	[Column("RANGOPERSONAINICIO")]
	public Nullable<Int32> Rangopersonainicio  { get; set; }

	[Display(Name = "RangoPersonaFin")]
	[Column("RANGOPERSONAFIN")]
	public Nullable<Int32> Rangopersonafin  { get; set; }

	[Display(Name = "PersonaActual")]
	[Column("PERSONAACTUAL")]
	public Nullable<Int32> Personaactual  { get; set; }

	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	public SyUnidadreplicacion() {
	}
 }
}
