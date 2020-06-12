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
     * @tabla dbo.accountmst
    */
    public class AccountmstPk {

	    [Key]
	    [Display(Name = "account")]
	    [MaxLength(20)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("ACCOUNT")]
	    public String Account  { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Account };
            return myObjArray;
        }
    }
}
