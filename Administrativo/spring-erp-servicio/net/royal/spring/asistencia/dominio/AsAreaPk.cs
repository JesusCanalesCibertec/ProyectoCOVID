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
     * @tabla dbo.AS_Area
    */
    public class AsAreaPk {

	    [Key]
	    [Display(Name = "Area")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("AREA")]

        public Nullable<Decimal> Area  { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Area };
            return myObjArray;
        }
    }
}
