using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.dominio
{

/**
 * 
 * 
 * @tabla dbo.UbicacionGeografica
*/
[Table("UBICACIONGEOGRAFICA")]
public class Ubicaciongeografica: UbicaciongeograficaPk {

	[Display(Name = "Descripcion")]
	[MaxLength(60)]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

	[Display(Name = "UltimaFechaModif")]
	[Column("ULTIMAFECHAMODIF")]
	public Nullable<DateTime> Ultimafechamodif  { get; set; }

	[Display(Name = "UltimoUsuario")]
	[MaxLength(60)]
	[Column("ULTIMOUSUARIO")]
	public String Ultimousuario  { get; set; }

	[Display(Name = "timestamp")]
	public byte[] Timestamp  { get; set; }

	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

        public Ubicaciongeografica()
        {
        }
 }
}
