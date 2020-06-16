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
 * @tabla dbo.AplicacionesMast
*/
[Table("APLICACIONESMAST")]
public class Aplicacionesmast: AplicacionesmastPk {

	[Display(Name = "DescripcionCorta")]
	[MaxLength(200)]
	[Column("DESCRIPCIONCORTA")]
	public String Descripcioncorta  { get; set; }

	[Display(Name = "DescripcionLarga")]
	[MaxLength(200)]
	[Column("DESCRIPCIONLARGA")]
	public String Descripcionlarga  { get; set; }

	[Display(Name = "UltimoPeriodoContable")]
	[MaxLength(6)]
	[Column("ULTIMOPERIODOCONTABLE")]
	public String Ultimoperiodocontable  { get; set; }

	[Display(Name = "SistemaFuente")]
	[MaxLength(8)]
	[Column("SISTEMAFUENTE")]
	public String Sistemafuente  { get; set; }

	[Display(Name = "EstaDisponible")]
	[MaxLength(10)]
	[Column("ESTADISPONIBLE")]
	public String Estadisponible  { get; set; }

	[Display(Name = "DepartamentoRevisor")]
	[MaxLength(3)]
	[Column("DEPARTAMENTOREVISOR")]
	public String Departamentorevisor  { get; set; }

	[Display(Name = "UltimoPeriodoProcesado")]
	[MaxLength(6)]
	[Column("ULTIMOPERIODOPROCESADO")]
	public String Ultimoperiodoprocesado  { get; set; }

	[Display(Name = "AplicacionUsuario")]
	[MaxLength(2)]
	[Column("APLICACIONUSUARIO")]
	public String Aplicacionusuario  { get; set; }

	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	[Display(Name = "AplicacionUsuario02")]
	[MaxLength(2)]
	[Column("APLICACIONUSUARIO02")]
	public String Aplicacionusuario02  { get; set; }

	[Display(Name = "AplicacionUsuario03")]
	[MaxLength(2)]
	[Column("APLICACIONUSUARIO03")]
	public String Aplicacionusuario03  { get; set; }

	[Display(Name = "AplicacionUsuario04")]
	[MaxLength(2)]
	[Column("APLICACIONUSUARIO04")]
	public String Aplicacionusuario04  { get; set; }

	public Aplicacionesmast() {
	}
 }
}
