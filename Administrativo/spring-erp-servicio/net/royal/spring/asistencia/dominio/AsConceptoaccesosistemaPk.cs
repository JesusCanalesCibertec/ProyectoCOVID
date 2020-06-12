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
    public class AsConceptoaccesosistemaPk {

        [Key]
        [Display(Name = "ConceptoSistema")]
        [MaxLength(4)]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("CONCEPTOSISTEMA")]
        public String Conceptosistema { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Conceptosistema };
            return myObjArray;
        }

    }
}
