using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.core.dominio
{

/**
 * 
 * 
 * @tabla dbo.MA_UnidadNegocio
*/
[Table("MA_UNIDADNEGOCIO")]
public class MaUnidadnegocio: MaUnidadnegocioPk {

	[Display(Name = "Zona")]
	[MaxLength(4)]
	[Column("ZONA")]
	public String Zona  { get; set; }

	[Display(Name = "DescripcionLocal")]
	[MaxLength(30)]
	[Column("DESCRIPCIONLOCAL")]
	public String Descripcionlocal  { get; set; }
        

	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	public MaUnidadnegocio() {
	}
 }
}
