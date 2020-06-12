using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.sistema.dominio
{

/**
 * 
 * 
 * @tabla dbo.ParametrosMast
*/
    public class ParametrosmastPk {

	    [Key]
	    [Display(Name = "AplicacionCodigo")]
	    [MaxLength(2)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("APLICACIONCODIGO")]
	    public String Aplicacioncodigo  { get; set; }

	    [Key]
	    [Display(Name = "ParametroClave")]
	    [MaxLength(10)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("PARAMETROCLAVE")]
	    public String Parametroclave  { get; set; }

	    [Key]
	    [Display(Name = "CompaniaCodigo")]
	    [MaxLength(10)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("COMPANIACODIGO")]
	    public String Companiacodigo  { get; set; }

        public ParametrosmastPk() { }
        public ParametrosmastPk(String _Parametroclave, String _Aplicacioncodigo, String _Companiacodigo) {
            Parametroclave = _Parametroclave;
            Aplicacioncodigo = _Aplicacioncodigo;
            Companiacodigo = _Companiacodigo;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Aplicacioncodigo, Parametroclave, Companiacodigo };
            return myObjArray;
        }
    }
}
