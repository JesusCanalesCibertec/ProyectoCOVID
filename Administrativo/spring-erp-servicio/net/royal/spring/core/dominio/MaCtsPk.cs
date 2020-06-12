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
    public class MaCtsPk {

        [Key]
        [Display(Name = "NumeroCTS")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("NUMEROCTS")]
        public Nullable<Int32> Numerocts { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Numerocts };
            return myObjArray;
        }
    }
}
