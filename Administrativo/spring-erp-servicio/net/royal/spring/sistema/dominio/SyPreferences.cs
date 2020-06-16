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
 * @tabla dbo.SY_Preferences
*/
[Table("SY_PREFERENCES")]
public class SyPreferences: SyPreferencesPk {

	[Display(Name = "AplicacionCodigo")]
	[MaxLength(2)]
	[Column("APLICACIONCODIGO")]
	public String Aplicacioncodigo  { get; set; }

	[Display(Name = "TipoValor")]
	[MaxLength(1)]
	[Column("TIPOVALOR")]
	public String Tipovalor  { get; set; }

	[Display(Name = "ValorString")]
	[MaxLength(15)]
	[Column("VALORSTRING")]
	public String Valorstring  { get; set; }

	[Display(Name = "ValorNumero")]
	[Column("VALORNUMERO")]
	public Nullable<Int32> Valornumero  { get; set; }

	[Display(Name = "ValorFecha")]
	[Column("VALORFECHA")]
	public Nullable<DateTime> Valorfecha  { get; set; }
        
	[Display(Name = "UltimoUsuario")]
	[MaxLength(20)]
	[Column("ULTIMOUSUARIO")]
	public String Ultimousuario  { get; set; }

	[Display(Name = "UltimaFechaModif")]
	[Column("ULTIMAFECHAMODIF")]
	public Nullable<DateTime> Ultimafechamodif  { get; set; }

        public SyPreferences()
        {
        }
 }
}
