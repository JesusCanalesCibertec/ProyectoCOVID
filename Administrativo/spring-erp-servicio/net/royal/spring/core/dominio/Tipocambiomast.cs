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
 * @tabla dbo.TipoCambioMast
*/
[Table("TIPOCAMBIOMAST")]
public class Tipocambiomast: TipocambiomastPk {

	[Display(Name = "FechaCambioString")]
	[MaxLength(8)]
	[Column("FECHACAMBIOSTRING")]
	public String Fechacambiostring  { get; set; }

	[Display(Name = "Factor")]
	[Column("FACTOR")]
	public Nullable<Single> Factor  { get; set; }

	[Display(Name = "FactorCompra")]
	[Column("FACTORCOMPRA")]
	public Nullable<Single> Factorcompra  { get; set; }

	[Display(Name = "FactorVenta")]
	[Column("FACTORVENTA")]
	public Nullable<Single> Factorventa  { get; set; }

	[Display(Name = "FactorPromedio")]
	[Column("FACTORPROMEDIO")]
	public Nullable<Single> Factorpromedio  { get; set; }
        
	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }
        
	public Tipocambiomast() {
	}
 }
}
