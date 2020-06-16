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
 * @tabla dbo.SY_AprobacionProceso_Valores
*/
[Table("SY_APROBACIONPROCESO_VALORES")]
public class SyAprobacionprocesoValores: SyAprobacionprocesoValoresPk {

	[Display(Name = "Descripcion")]
	[MaxLength(20)]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

	public SyAprobacionprocesoValores() {
	}
 }
}
