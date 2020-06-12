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
     * @tabla dbo.Banco
    */
    public class BancoPk {

        private String _Banco;

        [Key]
	    [Display(Name = "Banco")]
	    [MaxLength(3)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("BANCO")]
	    public String Banco  {
            get { return (_Banco == null) ? null : _Banco.Trim(); }
            set { _Banco = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Banco };
            return myObjArray;
        }
    }
}
