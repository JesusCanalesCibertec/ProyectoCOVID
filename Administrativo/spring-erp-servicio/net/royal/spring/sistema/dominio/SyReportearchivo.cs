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
     * @tabla dbo.SY_ReporteArchivo
*/
    [Table("SY_REPORTEARCHIVO")]
    public class SyReporteArchivo : SyReportearchivoPk
    {

        [Display(Name = "Reporte")]
        [Column("REPORTE")]
        public byte[] Reporte { get; set; }

        [Display(Name = "ESTADO")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Display(Name = "ULTIMOUSUARIO")]
        [MaxLength(50)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "ULTIMAFECHAMODIF")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        [NotMapped]
        public String AuxString { get; set; }

        public SyReporteArchivo()
        {
        }
    }
}
