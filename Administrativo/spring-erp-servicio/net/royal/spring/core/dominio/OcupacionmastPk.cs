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
     * @tabla dbo.OcupacionMast
    */
    public class OcupacionmastPk {

	    [Key]
	    [Display(Name = "Ocupacion")]
	    [MaxLength(4)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("OCUPACION")]
	    public String Ocupacion  { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Ocupacion };
            return myObjArray;
        }
    }
}
