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
     * @tabla dbo.MA_MiscelaneosHeader
    */
    public class MaMiscelaneosheaderPk {

	    [Key]
	    [Display(Name = "AplicacionCodigo")]
	    [MaxLength(2)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("APLICACIONCODIGO")]
	    public String Aplicacioncodigo  { get; set; }

	    [Key]
	    [Display(Name = "CodigoTabla")]
	    [MaxLength(10)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("CODIGOTABLA")]
	    public String Codigotabla  { get; set; }

	    [Key]
	    [Display(Name = "Compania")]
	    [MaxLength(6)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("COMPANIA")]
	    public String Compania  { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Aplicacioncodigo, Codigotabla, Compania };
            return myObjArray;
        }
    }
}
