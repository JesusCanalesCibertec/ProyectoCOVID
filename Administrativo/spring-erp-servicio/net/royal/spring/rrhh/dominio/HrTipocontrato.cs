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
     * @tabla dbo.HR_TipoContrato
*/
    [Table("HR_TIPOCONTRATO")]
    public class HrTipocontrato : HrTipocontratoPk
    {

        [Display(Name = "Descripcion")]
        [MaxLength(40)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Display(Name = "FlagFechaVencimiento")]
        [MaxLength(1)]
        [Column("FLAGFECHAVENCIMIENTO")]
        public String Flagfechavencimiento { get; set; }

        [Display(Name = "TipoContratoRTPS")]
        [MaxLength(2)]
        [Column("TIPOCONTRATORTPS")]
        public String Tipocontratortps { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [NotMapped]
        public int secuencia { get; set; }


        public HrTipocontrato()
        {
        }
    }
}
