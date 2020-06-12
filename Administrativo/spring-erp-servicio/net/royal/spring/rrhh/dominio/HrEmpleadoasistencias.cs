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
 * @tabla dbo.HR_EmpleadoAsistencias
*/
[Table("HR_EMPLEADOASISTENCIAS")]
public class HrEmpleadoasistencias: HrEmpleadoasistenciasPk {

	[Display(Name = "Periodo")]
	[MaxLength(6)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PERIODO")]
	public String Periodo  { get; set; }

	[Display(Name = "TotalDias")]
	[Column("TOTALDIAS")]
	public Nullable<Int32> Totaldias  { get; set; }

	[Display(Name = "TotalHoras")]
	[Column("TOTALHORAS")]
	public Nullable<Int32> Totalhoras  { get; set; }

	[Display(Name = "UnidadNegocio")]
	[MaxLength(4)]
	[Column("UNIDADNEGOCIO")]
	public String Unidadnegocio  { get; set; }

	[Display(Name = "UltimoUsuario")]
	[MaxLength(10)]
	[Column("ULTIMOUSUARIO")]
	public String Ultimousuario  { get; set; }

	[Display(Name = "UltimaFechaModif")]
	[Column("ULTIMAFECHAMODIF")]
	public Nullable<DateTime> Ultimafechamodif  { get; set; }

	[Display(Name = "Timestamp")]
	public byte[] Timestamp  { get; set; }

        public HrEmpleadoasistencias()
        {
        }
 }
}
