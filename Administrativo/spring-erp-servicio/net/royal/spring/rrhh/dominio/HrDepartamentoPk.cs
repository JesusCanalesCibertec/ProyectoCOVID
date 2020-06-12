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
     * @tabla dbo.HR_Departamento
*/
    public class HrDepartamentoPk
    {

        [Key]
        [Display(Name = "Departamento")]
        [MaxLength(10)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("DEPARTAMENTO")]
        public String Departamento { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Departamento };
            return myObjArray;
        }
    }
}
