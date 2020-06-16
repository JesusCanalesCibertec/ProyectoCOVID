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
 * @tabla dbo.HR_EncuestaPlantillaDetalle
*/
[Table("HR_ENCUESTAPLANTILLADETALLE")]
public class HrEncuestaplantilladetalle: HrEncuestaplantilladetallePk {

        [NotMapped]
        public String auxNombre { get; set; }

        public HrEncuestaplantilladetalle()
        {

        }
 }
}
