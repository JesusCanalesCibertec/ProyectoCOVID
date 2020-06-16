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
 * @tabla dbo.HR_CentroEstudios
*/
[Table("HR_CENTROESTUDIOS")]
public class HrCentroestudios: HrCentroestudiosPk {

	[Display(Name = "Descripcion")]
	[MaxLength(40)]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

	[Display(Name = "UltimoUsuario")]
	[MaxLength(10)]
	[Column("ULTIMOUSUARIO")]
	public String Ultimousuario  { get; set; }

	[Display(Name = "UltimaFechaModif")]
	[Column("ULTIMAFECHAMODIF")]
	public Nullable<DateTime> Ultimafechamodif  { get; set; }

	[Display(Name = "UnidadReplicacion")]
	[MaxLength(4)]
	[Column("UNIDADREPLICACION")]
	public String Unidadreplicacion  { get; set; }

	[Display(Name = "Lugar")]
	[MaxLength(20)]
	[Column("LUGAR")]
	public String Lugar  { get; set; }

	[Display(Name = "Pais")]
	[MaxLength(1)]
	[Column("PAIS")]
	public String Pais  { get; set; }

	[Display(Name = "CodigoPersona")]
	[Column("CODIGOPERSONA")]
	public Nullable<Int32> Codigopersona  { get; set; }

	[Display(Name = "RUC")]
	[MaxLength(11)]
	[Column("RUC")]
	public String Ruc  { get; set; }

	[Display(Name = "Representante")]
	[MaxLength(50)]
	[Column("REPRESENTANTE")]
	public String Representante  { get; set; }

	[Display(Name = "RepresentanteDoc")]
	[MaxLength(20)]
	[Column("REPRESENTANTEDOC")]
	public String Representantedoc  { get; set; }

        public HrCentroestudios()
        {
        }
 }
}
