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
     * @tabla dbo.SY_Reporte
    */
    [Table("SY_REPORTE")]
    public class SyReporte: SyReportePk {

	    [Display(Name = "DescripcionLocal")]
	    [MaxLength(50)]
	    [Column("DESCRIPCIONLOCAL")]
	    public String Descripcionlocal  { get; set; }

	    [Display(Name = "Tipo de Reporte")]
	    [MaxLength(5)]
	    [Column("TIPOREPORTE")]
	    public String Tiporeporte { get; set; }

        [MaxLength(100)]
        [Column("CARPETATEMPORAL")]
        public String CarpetaTemporal { get; set; }
                
        [Display(Name = "Estado")]
	    [MaxLength(1)]
	    [Column("ESTADO")]
	    public String Estado  { get; set; }
                
	    public SyReporte() {
	    }
    }
}
