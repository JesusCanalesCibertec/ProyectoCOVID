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
     * @tabla dbo.companyowner
    */
    public class CompanyownerPk {

	    [Key]
	    [Display(Name = "companyowner")]
	    [MaxLength(8)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("COMPANYOWNER")]
	    public String Companyowner  { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner };
            return myObjArray;
        }
    }
}
