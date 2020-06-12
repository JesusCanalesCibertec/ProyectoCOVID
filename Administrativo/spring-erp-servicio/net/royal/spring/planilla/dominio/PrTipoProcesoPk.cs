using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace net.royal.spring.planilla.dominio
{
    public class PrTipoProcesoPk
    {
        [Key]
        [Display(Name = "TipoProceso")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("TIPOPROCESO")]
        public String TipoProceso { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { TipoProceso };
            return myObjArray;
        }
    }
}
