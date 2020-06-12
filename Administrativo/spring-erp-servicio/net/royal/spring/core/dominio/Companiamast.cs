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
 * @tabla dbo.CompaniaMast
*/
[Table("COMPANIAMAST")]
public class Companiamast: CompaniamastPk {

	[Display(Name = "DescripcionCorta")]
	[MaxLength(30)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("DESCRIPCIONCORTA")]
	public String Descripcioncorta  { get; set; }

	[Display(Name = "DescripcionLarga")]
	[MaxLength(60)]
	[Column("DESCRIPCIONLARGA")]
	public String Descripcionlarga  { get; set; }

	[Display(Name = "DireccionComun")]
	[MaxLength(80)]
	[Column("DIRECCIONCOMUN")]
	public String Direccioncomun  { get; set; }
        
	[Display(Name = "DocumentoFiscal")]
	[MaxLength(20)]
	[Column("DOCUMENTOFISCAL")]
	public String Documentofiscal  { get; set; }
        
	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

        [Display(Name = "RepresentanteLegal")]
        [MaxLength(100)]
        [Column("REPRESENTANTELEGAL")]
        public String RepresentanteLegal { get; set; }

        public Companiamast() {
	}
 }
}
