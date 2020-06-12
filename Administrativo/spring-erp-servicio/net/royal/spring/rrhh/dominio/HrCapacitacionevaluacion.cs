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
 * @tabla dbo.HR_CapacitacionEvaluacion
*/
[Table("HR_CAPACITACIONEVALUACION")]
public class HrCapacitacionevaluacion: HrCapacitacionevaluacionPk {

	 
	[Display(Name = "Comentario")]
	[MaxLength(60)]
	[Column("COMENTARIO")]
	public String Comentario  { get; set; }

	 
	[Display(Name = "Empleado")]
	[Column("EMPLEADO")]
	public Nullable<Decimal> Empleado  { get; set; }

	 
	[Display(Name = "Respuesta")]
	[Column("RESPUESTA")]
	public Nullable<Decimal> Respuesta  { get; set; }

	 
	[Display(Name = "UltimoUsuario")]
	[MaxLength(20)]
	[Column("ULTIMOUSUARIO")]
	public String Ultimousuario  { get; set; }

	 
	[Display(Name = "UltimaFechaModif")]
	[Column("ULTIMAFECHAMODIF")]
	public Nullable<DateTime> Ultimafechamodif  { get; set; }

	 
	[Display(Name = "JEFERESPONSABLE")]
	[Column("JEFERESPONSABLE")]
	public Nullable<Int32> Jeferesponsable  { get; set; }

	 
	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

        public HrCapacitacionevaluacion()
        {
        }
 }
}
