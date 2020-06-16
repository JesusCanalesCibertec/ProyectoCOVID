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
     * @tabla dbo.AS_ConceptoAccesoSistema
    */
    [Table("AS_CONCEPTOACCESOSISTEMA")]
    public class AsConceptoaccesosistema: AsConceptoaccesosistemaPk {
        
	    [Display(Name = "Descripcion")]
        [MaxLength(50)]
	    [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("DESCRIPCION")]

        public String Descripcion  { get; set; }

	    [Display(Name = "ConceptoAccesoDefault")]
        [MaxLength(4)]
	    [Column("CONCEPTOACCESODEFAULT")]

        public String Conceptoaccesodefault  { get; set; }

	    [Display(Name = "RequiereJustificacion")]
        [MaxLength(1)]
	    [Column("REQUIEREJUSTIFICACION")]

        public String Requierejustificacion  { get; set; }

        [Display(Name = "AutoJustificable")]
        [MaxLength(1)]
        [Column("AUTOJUSTIFICABLE")]
        public String Autojustificable { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
	    [Column("ESTADO")]

        public String Estado  { get; set; }

        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }
              

	    public AsConceptoaccesosistema() {
	    }
 }
}
