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
 * @tabla dbo.SeguridadAutorizacionReporte
*/
public class SeguridadautorizacionreportePk {

	[Key]
	[Display(Name = "Usuario")]
	[MaxLength(20)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("USUARIO")]
	public String Usuario  { get; set; }

	[Key]
	[Display(Name = "AplicacionCodigo")]
	[MaxLength(2)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("APLICACIONCODIGO")]
	public String Aplicacioncodigo  { get; set; }

	[Key]
	[Display(Name = "ReporteCodigo")]
	[MaxLength(3)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("REPORTECODIGO")]
	public String Reportecodigo  { get; set; }
 }
}
