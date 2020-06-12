using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.planilla.dominio
{

    /**
     * 
     * 
     * @tabla dbo.PR_TipoPlanilla
    */
    [Table("PR_TIPOPLANILLA")]
    public class PrTipoplanilla: PrTipoplanillaPk {

	    [Display(Name = "Funcion")]
	    [Column("FUNCION")]
	    public Nullable<Int32> Funcion  { get; set; }

	    [Display(Name = "Descripcion")]
	    [MaxLength(30)]
	    [Column("DESCRIPCION")]
	    public String Descripcion  { get; set; }

	    [Display(Name = "TitulodeBoleta")]
	    [MaxLength(60)]
	    [Column("TITULODEBOLETA")]
	    public String Titulodeboleta  { get; set; }
        
	    [Display(Name = "Estado")]
	    [MaxLength(1)]
	    [Column("ESTADO")]
	    public String Estado  { get; set; }

        [Display(Name = "Periodicidad")]
        [MaxLength(1)]
        [Column("Periodicidad")]
        public String Periodicidad { get; set; }

        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public PrTipoplanilla() {
	    }
     }
}
