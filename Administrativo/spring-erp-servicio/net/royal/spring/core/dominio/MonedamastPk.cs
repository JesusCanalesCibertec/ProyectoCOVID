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
     * @tabla dbo.MonedaMast
    */
    public class MonedamastPk {

	    [Key]
	    [Display(Name = "MonedaCodigo")]
	    [MaxLength(2)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("MONEDACODIGO")]
	    public String Monedacodigo  { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Monedacodigo };
            return myObjArray;
        }
    }
}
