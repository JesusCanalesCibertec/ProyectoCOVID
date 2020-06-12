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
     * @tabla dbo.TipoCambioMast
    */
    public class TipocambiomastPk {

	    [Key]
	    [Display(Name = "MonedaCodigo")]
	    [MaxLength(2)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("MONEDACODIGO")]
	    public String Monedacodigo  { get; set; }

	    [Key]
	    [Display(Name = "MonedaCambioCodigo")]
	    [MaxLength(2)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("MONEDACAMBIOCODIGO")]
	    public String Monedacambiocodigo  { get; set; }

	    [Key]
	    [Display(Name = "FechaCambio")]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("FECHACAMBIO")]
	    public Nullable<DateTime> Fechacambio  { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Monedacodigo, Monedacambiocodigo, Fechacambio };
            return myObjArray;
        }
    }
}
