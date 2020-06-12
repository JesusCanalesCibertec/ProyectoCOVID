using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.core.dominio
{

    /**
     * 
     * 
     * @tabla dbo.MA_CTS
    */
    [Table("MA_CTS")]
    public class MaCts: MaCtsPk {

            [Display(Name = "Regimen")]
	    [MaxLength(1)]
	    [Column("REGIMEN")]
	    public String Regimen  { get; set; }

	    [Display(Name = "Descripcion")]
	    [MaxLength(100)]
	    [Column("DESCRIPCION")]
	    public String Descripcion  { get; set; }

	    [Display(Name = "FechaInicio")]
	    [Column("FECHAINICIO")]
	    public Nullable<DateTime> Fechainicio  { get; set; }

	    [Display(Name = "FechaFin")]
	    [Column("FECHAFIN")]
	    public Nullable<DateTime> Fechafin  { get; set; }

	    [Display(Name = "FechaPago")]
	    [Column("FECHAPAGO")]
	    public Nullable<DateTime> Fechapago  { get; set; }

	    [Display(Name = "Estado")]
	    [MaxLength(1)]
	    [Column("ESTADO")]
	    public String Estado  { get; set; }

	    [Display(Name = "EstadoCTS")]
	    [MaxLength(1)]
	    [Column("ESTADOCTS")]
	    public String Estadocts  { get; set; }
        
	    [Display(Name = "periodoplanilla")]
	    [MaxLength(6)]
	    [Column("PERIODOPLANILLA")]
	    public String Periodoplanilla  { get; set; }
        
	    public MaCts() {
	    }
     }
}
