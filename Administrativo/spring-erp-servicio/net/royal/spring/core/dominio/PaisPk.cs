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
     * @tabla dbo.Pais
    */
    public class PaisPk {

        private String _Pais;

	    [Key]
	    [Display(Name = "Pais")]
	    [MaxLength(4)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("PAIS")]
	    public String Pais
        {
            get { return (_Pais == null) ? "" : _Pais.Trim(); }
            set { _Pais = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Pais };
            return myObjArray;
        }
    }
}
