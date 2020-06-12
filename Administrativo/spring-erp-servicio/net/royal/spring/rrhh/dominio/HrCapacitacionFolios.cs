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
     * @tabla dbo.HR_Capacitacion
*/
    [Table("HR_CAPACITACIONFOLIOS")]
    public class HrCapacitacionFolios : HrCapacitacionFoliosPk
    {

        public static String PARAMETRO_RUTA_ADJUNTO = "PATHCAPADJ";

        [Column("ARCHIVO")]
        public String Archivo { get; set; }

        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        [NotMapped]
        public String auxData { get; set; }

        public HrCapacitacionFolios()
        {

        }
    }
}
