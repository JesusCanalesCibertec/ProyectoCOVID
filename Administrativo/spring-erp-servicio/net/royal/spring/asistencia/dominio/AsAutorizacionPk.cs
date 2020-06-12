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
     * @tabla dbo.AS_Autorizacion
*/
    public class AsAutorizacionPk
    {
        // [Key]
        [Display(Name = "Empleado")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("EMPLEADO")]
        //public Nullable<Int32> Empleado { get; set; }
        public Nullable<Decimal> Empleado { get; set; }

        // [Key]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("FECHA")]
        public Nullable<DateTime> Fecha { get; set; }

       // [Key]
        [Display(Name = "ConceptoAcceso")]
        [MaxLength(4)]
        [Column("CONCEPTOACCESO")]
        public String Conceptoacceso { get; set; }

       // [Key]
        [Display(Name = "Desde")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("DESDE")]
        public Nullable<DateTime> Desde { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Empleado, Fecha, Conceptoacceso, Desde };
            return myObjArray;
        }

    }
}
