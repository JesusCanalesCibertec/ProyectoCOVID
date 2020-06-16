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
 * @tabla dbo.SY_PLANTILLA
*/
[Table("SY_PLANTILLA")]
public class SyPlantilla: SyPlantillaPk {

	

	[Display(Name = "nombre")]
	[MaxLength(200)]
	[Column("NOMBRE")]
	public String Nombre  { get; set; }

	[Display(Name = "DESCRIPCION")]
	[MaxLength(500)]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

	[Display(Name = "TIPOPLANTILLA")]
	[MaxLength(4)]
	[Column("TIPOPLANTILLA")]
	public String Tipoplantilla  { get; set; }

	[Display(Name = "plantilla")]
	[Column("PLANTILLA")]
	public byte[] Plantilla  { get; set; }

	[Display(Name = "ESTADO")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

        [Display(Name = "ULTIMOUSUARIO")]
        [MaxLength(50)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "ULTIMAFECHAMODIF")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        [NotMapped]
        public String AuxString { get; set; }

        public SyPlantilla() {
	}
 }
}
