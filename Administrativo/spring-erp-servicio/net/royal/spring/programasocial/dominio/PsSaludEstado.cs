using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio
{

/**
 * 
 * 
 * @tabla sgseguridadsys.PS_SALUD_ESTADO
*/
[Table("PS_SALUD_ESTADO", Schema = "sgseguridadsys")]
public class PsSaludEstado: PsSaludEstadoPk {

	
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

        public PsSaludEstado()
        {
        }
 }
}
