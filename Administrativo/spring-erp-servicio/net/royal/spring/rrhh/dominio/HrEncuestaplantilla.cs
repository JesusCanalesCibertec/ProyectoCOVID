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
 * @tabla dbo.HR_EncuestaPlantilla
*/
[Table("HR_ENCUESTAPLANTILLA")]
public class HrEncuestaplantilla: HrEncuestaplantillaPk {

	[Display(Name = "Descripcion")]
	[MaxLength(200)]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	[Display(Name = "UltimoUsuario")]
	[MaxLength(20)]
	[Column("ULTIMOUSUARIO")]
	public String Ultimousuario  { get; set; }

	[Display(Name = "UltimaFechaModif")]
	[Column("ULTIMAFECHAMODIF")]
	public Nullable<DateTime> Ultimafechamodif  { get; set; }

	[Display(Name = "flagcapacitacion")]
	[MaxLength(1)]
	[Column("FLAGCAPACITACION")]
	public String Flagcapacitacion  { get; set; }

        [NotMapped]
        public List<HrEncuestaplantilladetalle> listadetalle;

        public HrEncuestaplantilla()
        {
            listadetalle = new List<HrEncuestaplantilladetalle>();
        }
    }
}
