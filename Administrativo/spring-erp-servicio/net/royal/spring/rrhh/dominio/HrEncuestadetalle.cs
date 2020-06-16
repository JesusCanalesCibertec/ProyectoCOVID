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
     * @tabla dbo.HR_EncuestaDetalle
*/
    [Table("HR_ENCUESTADETALLE")]
    public class HrEncuestadetalle : HrEncuestadetallePk
    {
        [Column("Orden")]
        public Nullable<Int32> orden { get; set; }

        [Column("ID_INDICADOR")]
        public String idIndicador { get; set; }

        [Column("GRUPO")]
        public String grupo { get; set; }

        [NotMapped]
        public String auxPregunta { get; set; }

        [NotMapped]
        public String auxArea { get; set; }

        public HrEncuestadetalle()
        {
        }
    }
}
