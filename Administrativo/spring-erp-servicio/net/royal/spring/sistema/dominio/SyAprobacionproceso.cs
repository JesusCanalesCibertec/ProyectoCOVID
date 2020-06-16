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
 * @tabla dbo.SY_AprobacionProceso
*/
[Table("SY_APROBACIONPROCESO")]
public class SyAprobacionproceso: SyAprobacionprocesoPk {

	[Display(Name = "Proceso")]
	[MaxLength(50)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PROCESO")]
	public String Proceso  { get; set; }

	[Display(Name = "ProcesoInterno")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PROCESOINTERNO")]
	public Nullable<Int32> Procesointerno  { get; set; }

	[Display(Name = "FlagTieneDetalle")]
	[MaxLength(1)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("FLAGTIENEDETALLE")]
	public String Flagtienedetalle  { get; set; }

	[Display(Name = "FlagTipoDetalle")]
	[MaxLength(1)]
	[Column("FLAGTIPODETALLE")]
	public String Flagtipodetalle  { get; set; }

	[Display(Name = "NombreTabla")]
	[MaxLength(40)]
	[Column("NOMBRETABLA")]
	public String Nombretabla  { get; set; }

	[Display(Name = "NombreDW")]
	[MaxLength(50)]
	[Column("NOMBREDW")]
	public String Nombredw  { get; set; }

	[Display(Name = "CodigoTabla")]
	[MaxLength(50)]
	[Column("CODIGOTABLA")]
	public String Codigotabla  { get; set; }

	[Display(Name = "CampoCompara")]
	[MaxLength(20)]
	[Column("CAMPOCOMPARA")]
	public String Campocompara  { get; set; }

	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }
    
    /*
     * Estos campos no se usan en el caso de nombre de servicio se debe obtener de los miscelaneos
	[Display(Name = "Window")]
	[MaxLength(100)]
	[Column("WINDOW")]
	public String Window  { get; set; }

	[Display(Name = "NOMBRESERVICIO")]
	[MaxLength(300)]
	[Column("NOMBRESERVICIO")]
	public String Nombreservicio  { get; set; }
    */

	public SyAprobacionproceso() {
	}
 }
}
