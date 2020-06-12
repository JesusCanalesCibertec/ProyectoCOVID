using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.rrhh.dominio
{

    /**
     * 
     * 
     * @tabla dbo.HR_Empleado
*/
    public class HrEmpleadoPk
    {

        [Key]
        [Display(Name = "Empleado")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("EMPLEADO")]
        public Nullable<Int32> Empleado { get; set; }

        public HrEmpleadoPk()
        {

        }

        public HrEmpleadoPk(int? Empleado)
        {
            this.Empleado = Empleado;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Empleado };
            return myObjArray;
        }
    }
}
