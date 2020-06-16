using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.asistencia.dominio
{

    /**
     * 
     * 
     * @tabla dbo.AS_ConceptoAcceso
*/
    [Table("AS_CONCEPTOACCESO_REGLA")]
    public class AsConceptoaccesoRegla : AsConceptoaccesoReglaPk
    {
        [Column("FECHAINICIO")]
        public Nullable<DateTime> Fechainicio { get; set; }

        [Column("FECHAFIN")]
        public Nullable<DateTime> Fechafin { get; set; }

        [Column("HORAINICIO")]
        public Nullable<DateTime> Horainicio { get; set; }

        [Column("HORAFIN")]
        public Nullable<DateTime> Horafin { get; set; }

        [Column("CANTIDADINICIO")]
        public Nullable<Int32> Cantidadinicio { get; set; }

        [Column("CANTIDADFIN")]
        public Nullable<Int32> Cantidadfin { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public AsConceptoaccesoRegla()
        {
        }
        
    }
}
