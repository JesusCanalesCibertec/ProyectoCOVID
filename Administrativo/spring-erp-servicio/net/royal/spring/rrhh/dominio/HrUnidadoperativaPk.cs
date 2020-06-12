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
     * @tabla dbo.HR_UNIDADOPERATIVA
*/
    public class HrUnidadoperativaPk
    {

        [Key]
        [Display(Name = "UNIDADOPERATIVA")]
        [MaxLength(4)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("UNIDADOPERATIVA")]
        public String Unidadoperativa { get; set; }

        public HrUnidadoperativaPk() { }
        public HrUnidadoperativaPk(String _Unidadoperativa) {
            Unidadoperativa = _Unidadoperativa;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Unidadoperativa };
            return myObjArray;
        }
    }
}
