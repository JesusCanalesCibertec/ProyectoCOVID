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
 * @tabla dbo.HR_GradoInstruccion
*/
[Table("HR_GRADOINSTRUCCION")]
public class HrGradoinstruccion: HrGradoinstruccionPk {

	[Display(Name = "Descripcion")]
	[MaxLength(40)]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

	[Display(Name = "FlagCentroEstudios")]
	[MaxLength(1)]
	[Column("FLAGCENTROESTUDIOS")]
	public String Flagcentroestudios  { get; set; }

	[Display(Name = "FlagTieneCarrera")]
	[MaxLength(1)]
	[Column("FLAGTIENECARRERA")]
	public String Flagtienecarrera  { get; set; }

	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

        [Display(Name = "Ultimo usuario")]
        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "Ultimo fecha de modificación")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        [Display(Name = "NivelEducativoRTPS")]
	[MaxLength(2)]
	[Column("NIVELEDUCATIVORTPS")]
	public String Niveleducativortps  { get; set; }

	[Display(Name = "gradoinstruccionsuperior")]
	[MaxLength(5)]
	[Column("GRADOINSTRUCCIONSUPERIOR")]
	public String Gradoinstruccionsuperior  { get; set; }

	public HrGradoinstruccion() {
	}
 }
}
