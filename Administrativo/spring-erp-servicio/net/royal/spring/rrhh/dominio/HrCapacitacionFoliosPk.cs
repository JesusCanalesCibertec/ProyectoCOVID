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
     * @tabla dbo.HR_Capacitacion
*/
    public class HrCapacitacionFoliosPk
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
        [Column("SECUENCIA")]
        public Nullable<Int32> Secuencia { get; set; }

        public HrCapacitacionFoliosPk()
        {
        }

        public HrCapacitacionFoliosPk(String pCompanyowner, String pCapacitacion, Int32 secuencia)
        {
            this.Companyowner = pCompanyowner;
            this.Capacitacion = pCapacitacion;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner, Capacitacion, Secuencia };
            return myObjArray;
        }
    }
}
