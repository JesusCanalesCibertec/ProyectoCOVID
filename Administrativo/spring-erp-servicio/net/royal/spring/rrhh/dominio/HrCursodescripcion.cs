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
 * @tabla dbo.HR_CursoDescripcion
*/
[Table("HR_CURSODESCRIPCION")]
public class HrCursodescripcion: HrCursodescripcionPk {


	[Display(Name = "Descripcion")]
	[MaxLength(60)]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }


	[Display(Name = "DescripcionIngles")]
	[MaxLength(60)]
	[Column("DESCRIPCIONINGLES")]
	public String Descripcioningles  { get; set; }


	[Display(Name = "Tipo")]
	[MaxLength(10)]
	[Column("TIPO")]
	public String Tipo  { get; set; }


	[Display(Name = "UnidadNegocio")]
	[MaxLength(4)]
	[Column("UNIDADNEGOCIO")]
	public String Unidadnegocio  { get; set; }

	
	[Display(Name = "NivelAcademico")]
	[MaxLength(5)]
	[Column("NIVELACADEMICO")]
	public String Nivelacademico  { get; set; }


	[Display(Name = "Area")]
	[MaxLength(10)]
	[Column("AREA")]
	public String Area  { get; set; }


	[Display(Name = "Carrera")]
	[MaxLength(10)]
	[Column("CARRERA")]
	public String Carrera  { get; set; }

	
	[Display(Name = "CostoEstimado")]
	[Column("COSTOESTIMADO")]
	public Nullable<Decimal> Costoestimado  { get; set; }

	
	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	
	[Display(Name = "UltimoUsuario")]
	[MaxLength(10)]
	[Column("ULTIMOUSUARIO")]
	public String Ultimousuario  { get; set; }


	[Display(Name = "UltimaFechaModif")]
	[Column("ULTIMAFECHAMODIF")]
	public Nullable<DateTime> Ultimafechamodif  { get; set; }

        public HrCursodescripcion()
        {
        }
 }
}
