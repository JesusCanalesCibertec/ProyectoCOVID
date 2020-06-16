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
 * @tabla dbo.HR_CapacitacionEval
*/
[Table("HR_CAPACITACIONEVAL")]
public class HrCapacitacioneval: HrCapacitacionevalPk {

	 
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

        public HrCapacitacioneval()
        {
        }
 }
}
