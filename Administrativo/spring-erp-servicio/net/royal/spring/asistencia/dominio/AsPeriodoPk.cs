using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.asistencia.dominio
{

    /**
     * 
     * 
     * @tabla dbo.AS_Periodo
    */
    public class AsPeriodoPk
    {

        [Key]
        [Display(Name = "Secuencia")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("SECUENCIA")]
        public Nullable<Decimal> Secuencia { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Secuencia };
            return myObjArray;
        }
    }
}
