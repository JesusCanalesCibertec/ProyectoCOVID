using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.planilla.dominio
{

    /**
     * 
     * 
     * @tabla dbo.PR_TipoPrestamo
*/
    public class PrTipoprestamoPk
    {

        [Key]
        [Display(Name = "TipoPrestamo")]
        [MaxLength(6)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("TIPOPRESTAMO")]
        public String Tipoprestamo { get; set; }

        public PrTipoprestamoPk() { }

        public PrTipoprestamoPk(String tipo_prestamo)
        {
            Tipoprestamo = tipo_prestamo;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Tipoprestamo };
            return myObjArray;
        }

    }
}
