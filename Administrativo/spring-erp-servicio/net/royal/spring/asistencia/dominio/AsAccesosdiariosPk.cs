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
 * @tabla dbo.AS_AccesosDiarios
*/
    public class AsAccesosdiariosPk {

	    [Key]
	    [Display(Name = "CarnetId")]
        [MaxLength(15)]
	    [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("CARNETID")]

        public String Carnetid  { get; set; }

	    [Key]
	    [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("FECHA")]

        public Nullable<DateTime> Fecha  { get; set; }

	    [Key]
	    [Display(Name = "Secuencia")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("SECUENCIA")]

        public Nullable<Decimal> Secuencia  { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Carnetid, Fecha, Secuencia };
            return myObjArray;
        }
    }
}
