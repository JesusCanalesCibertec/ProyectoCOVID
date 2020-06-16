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
     * @tabla dbo.HR_EmpleadoCapacitacion
*/
    public class HrEmpleadocapacitacionPk
    {

        [Key]
        [Display(Name = "CompanyOwner")]
        [MaxLength(8)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("COMPANYOWNER")]
        public String Companyowner { get; set; }

        [Key]
        [Display(Name = "Capacitacion")]
        [MaxLength(10)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("CAPACITACION")]
        public String Capacitacion { get; set; }

        [Key]
        [Display(Name = "Empleado")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("EMPLEADO")]
        public Nullable<Int32> Empleado { get; set; }

        public HrEmpleadocapacitacionPk()
        {
        }

        public HrEmpleadocapacitacionPk(String pCompanyowner, String pCapacitacion, Nullable<Int32> pEmpleado)
        {
            this.Companyowner = pCompanyowner;
            this.Capacitacion = pCapacitacion;
            this.Empleado = pEmpleado;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner, Capacitacion, Empleado };
            return myObjArray;
        }
    }
}
