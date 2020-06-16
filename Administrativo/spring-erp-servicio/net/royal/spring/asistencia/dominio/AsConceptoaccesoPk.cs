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
    public class AsConceptoaccesoPk {
        [Key]
        [Display(Name = "ConceptoAcceso")]
        [MaxLength(4)]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("CONCEPTOACCESO")]
        public String Conceptoacceso { get; set; }

        public AsConceptoaccesoPk() { }
        public AsConceptoaccesoPk(String _Conceptoacceso) {
            Conceptoacceso = _Conceptoacceso;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Conceptoacceso };
            return myObjArray;
        }
    }
}
