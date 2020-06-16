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
 * @tabla dbo.UnidadesMast
*/
[Table("UNIDADESMAST")]
public class Unidadesmast: UnidadesmastPk {


	[Display(Name = "DescripcionCorta")]
	[MaxLength(20)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("DESCRIPCIONCORTA")]
	public String Descripcioncorta  { get; set; }


	[Display(Name = "DescripcionExtranjera")]
	[MaxLength(40)]
	[Column("DESCRIPCIONEXTRANJERA")]
	public String Descripcionextranjera  { get; set; }


	[Display(Name = "UnidadTipo")]
	[MaxLength(1)]
	[Column("UNIDADTIPO")]
	public String Unidadtipo  { get; set; }


	[Display(Name = "NumeroDecimales")]
	[Column("NUMERODECIMALES")]
	public Nullable<Int32> Numerodecimales  { get; set; }

	
	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	
	[Display(Name = "UltimaFechaModif")]
	[Column("ULTIMAFECHAMODIF")]
	public Nullable<DateTime> Ultimafechamodif  { get; set; }


	[Display(Name = "UltimoUsuario")]
	[MaxLength(10)]
	[Column("ULTIMOUSUARIO")]
	public String Ultimousuario  { get; set; }


	[Display(Name = "Timestamp")]
	public byte[] Timestamp  { get; set; }


	[Display(Name = "CODIGOFISCAL")]
	[MaxLength(3)]
	[Column("CODIGOFISCAL")]
	public String Codigofiscal  { get; set; }


	[Display(Name = "UnidadFe")]
	[MaxLength(3)]
	[Column("UNIDADFE")]
	public String Unidadfe  { get; set; }

        public Unidadesmast()
        {
        }
 }
}
