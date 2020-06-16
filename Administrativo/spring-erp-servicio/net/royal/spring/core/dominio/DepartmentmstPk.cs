using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.core.dominio
{

    /**
     * 
     * 
     * @tabla dbo.departmentmst
    */
    public class DepartmentmstPk
    {
        private string _Department;
        [Key]
        [Display(Name = "department")]
        [MaxLength(3)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("DEPARTMENT")]
        public String Department
        {
            get { return (_Department == null) ? "" : _Department.Trim(); }
            set { _Department = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Department };
            return myObjArray;
        }
    }
}
