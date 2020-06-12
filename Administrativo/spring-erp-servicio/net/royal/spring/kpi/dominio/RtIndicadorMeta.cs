using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.kpi.dominio
{

/**
 * 
 * 
 * @tabla sgseguridadsys.RT_INDICADOR_META
*/
[Table("RT_INDICADOR_META", Schema = "sgseguridadsys")]
public class RtIndicadorMeta: RtIndicadorMetaPk {

	[Display(Name = "INICIAL")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("INICIAL")]
	public Nullable<Decimal> Inicial  { get; set; }

	[Display(Name = "FINAL")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("FINAL")]
	public Nullable<Decimal> Final  { get; set; }

	[Display(Name = "COLOR")]
	[MaxLength(50)]
	[Column("COLOR")]
	public String Color  { get; set; }

	[Display(Name = "CREACION_USUARIO")]
	[MaxLength(50)]
	[Column("CREACION_USUARIO")]
	public String CreacionUsuario  { get; set; }

	[Display(Name = "CREACION_FECHA")]
	[Column("CREACION_FECHA")]
	public Nullable<DateTime> CreacionFecha  { get; set; }

	[Display(Name = "CREACION_TERMINAL")]
	[MaxLength(50)]
	[Column("CREACION_TERMINAL")]
	public String CreacionTerminal  { get; set; }

	[Display(Name = "MODIFICACION_USUARIO")]
	[MaxLength(50)]
	[Column("MODIFICACION_USUARIO")]
	public String ModificacionUsuario  { get; set; }

	[Display(Name = "MODIFICACION_FECHA")]
	[Column("MODIFICACION_FECHA")]
	public Nullable<DateTime> ModificacionFecha  { get; set; }

	[Display(Name = "MODIFICACION_TERMINAL")]
	[MaxLength(50)]
	[Column("MODIFICACION_TERMINAL")]
	public String ModificacionTerminal  { get; set; }

        public RtIndicadorMeta()
        {
        }
 }
}
