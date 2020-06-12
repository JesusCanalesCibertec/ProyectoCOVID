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
     * @tabla dbo.AS_ConceptoAccesoRegla
    */
    public class AsConceptoaccesoReglaPk {
        [Key]
        [Display(Name = "ConceptoAcceso")]
        [MaxLength(5)]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("CONCEPTOACCESO")]
        public String Conceptoacceso { get; set; }

        [Key]
        [Display(Name = "Regla")]
        [MaxLength(10)]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("REGLA")]
        public String Regla { get; set; }

        public AsConceptoaccesoReglaPk() { }
        public AsConceptoaccesoReglaPk(String _Conceptoacceso, String _Regla) {
            Conceptoacceso = _Conceptoacceso;
            Regla = _Regla;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Conceptoacceso, Regla };
            return myObjArray;
        }
    }
}
