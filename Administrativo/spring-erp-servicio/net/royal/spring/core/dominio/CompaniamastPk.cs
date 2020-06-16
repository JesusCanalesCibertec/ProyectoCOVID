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
     * @tabla dbo.CompaniaMast
    */
    public class CompaniamastPk {

	    [Key]
	    [Display(Name = "CompaniaCodigo")]
	    [MaxLength(6)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("COMPANIACODIGO")]
	    public String Companiacodigo  { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companiacodigo };
            return myObjArray;
        }
    }
}
