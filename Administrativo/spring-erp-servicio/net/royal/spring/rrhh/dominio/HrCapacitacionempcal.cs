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
 * @tabla dbo.HR_CapacitacionEmpCal
*/
[Table("HR_CAPACITACIONEMPCAL")]
public class HrCapacitacionempcal: HrCapacitacionempcalPk {

	 
	[Display(Name = "Calificacion")]
	[Column("CALIFICACION")]
	public Nullable<Decimal> Calificacion  { get; set; }

	 
	[Display(Name = "FechaEvaluacion")]
	[Column("FECHAEVALUACION")]
	public Nullable<DateTime> Fechaevaluacion  { get; set; }

	 
	[Display(Name = "Observacion")]
	[MaxLength(500)]
	[Column("OBSERVACION")]
	public String Observacion  { get; set; }

	 
	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	 
	[Display(Name = "UltimoUsuario")]
	[MaxLength(20)]
	[Column("ULTIMOUSUARIO")]
	public String Ultimousuario  { get; set; }

	 
	[Display(Name = "UltimaFechaModif")]
	[Column("ULTIMAFECHAMODIF")]
	public Nullable<DateTime> Ultimafechamodif  { get; set; }

	 
	[Display(Name = "FlagAprobado")]
	[MaxLength(1)]
	[Column("FLAGAPROBADO")]
	public String Flagaprobado  { get; set; }

	 
	[Display(Name = "Motivo")]
	[MaxLength(800)]
	[Column("MOTIVO")]
	public String Motivo  { get; set; }

	 
	[Display(Name = "archivo")]
	[MaxLength(1000)]
	[Column("ARCHIVO")]
	public String Archivo  { get; set; }

        public HrCapacitacionempcal()
        {
        }
 }
}
