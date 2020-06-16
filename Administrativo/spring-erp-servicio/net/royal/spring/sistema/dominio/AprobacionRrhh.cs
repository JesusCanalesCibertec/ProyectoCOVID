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
 * @tabla dbo.APROBACION_RRHH
*/
[Table("APROBACION_RRHH")]
public class AprobacionRrhh: AprobacionRrhhPk {

	[Display(Name = "APLICACION")]
	[MaxLength(2)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("APLICACION")]
	public String Aplicacion  { get; set; }

	[Display(Name = "PROCESO")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PROCESO")]
	public Nullable<Int32> Proceso  { get; set; }

	[Display(Name = "PROCESOINTERNO")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PROCESOINTERNO")]
	public Nullable<Int32> Procesointerno  { get; set; }

	[Display(Name = "COMPANIASOCIO")]
	[MaxLength(8)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("COMPANIASOCIO")]
	public String Companiasocio  { get; set; }

	[Display(Name = "CODIGOPROCESO")]
	[MaxLength(2)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("CODIGOPROCESO")]
	public String Codigoproceso  { get; set; }

	[Display(Name = "NUMEROPROCESO")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("NUMEROPROCESO")]
	public Nullable<Int32> Numeroproceso  { get; set; }

	[Display(Name = "NIVEL")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("NIVEL")]
	public Nullable<Int32> Nivel  { get; set; }

	[Display(Name = "ESTADONIVEL")]
	[MaxLength(1)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ESTADONIVEL")]
	public String Estadonivel  { get; set; }

	[Display(Name = "USUARIOAPROBADOR")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("USUARIOAPROBADOR")]
	public Nullable<Int32> Usuarioaprobador  { get; set; }

	[Display(Name = "OBSERVACION")]
	[MaxLength(500)]
	[Column("OBSERVACION")]
	public String Observacion  { get; set; }

	[Display(Name = "MOTIVO_RECHAZO")]
	[MaxLength(5000)]
	[Column("MOTIVO_RECHAZO")]
	public String MotivoRechazo  { get; set; }

	[Display(Name = "MOTIVO_REVERSION")]
	[MaxLength(5000)]
	[Column("MOTIVO_REVERSION")]
	public String MotivoReversion  { get; set; }

        [Display(Name = "ULTIMOUSUARIO")]
        [MaxLength(20)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "ULTIMAFECHAMODIF")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public AprobacionRrhh() {
	}
 }
}
