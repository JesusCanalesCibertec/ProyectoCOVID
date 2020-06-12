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
     * @tabla dbo.AS_AsistenciaDiariaMarca
    */
    public class AsAsistenciadiariamarcaPk {

	    [Key]
	    [Display(Name = "Empleado")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("EMPLEADO")]

        public Nullable<Int32> Empleado  { get; set; }

	    [Key]
	    [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("FECHA")]

        public Nullable<DateTime> Fecha  { get; set; }

	    [Key]
	    [Display(Name = "Hora")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("HORA")]

        public Nullable<DateTime> Hora  { get; set; }

	    [Key]
	    [Display(Name = "Secuencia")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("SECUENCIA")]

        public Nullable<Int32> Secuencia  { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Empleado, Fecha, Hora, Secuencia };
            return myObjArray;
        }
    }
}
