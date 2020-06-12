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
     * @tabla dbo.AS_AsistenciaDiaria
    */
    public class AsAsistenciadiariaPk {

        [Display(Name = "Empleado")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("EMPLEADO")]
        public Nullable<Int32> Empleado { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("FECHA")]
        public Nullable<DateTime> Fecha { get; set; }

        [Display(Name = "Secuencia")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("SECUENCIA")]
        public Nullable<Int32> Secuencia { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Empleado, Fecha, Secuencia };
            return myObjArray;
        }
    }
}
