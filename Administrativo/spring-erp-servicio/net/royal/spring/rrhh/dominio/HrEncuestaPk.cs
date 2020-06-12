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
     * @tabla dbo.HR_Encuesta
*/
    public class HrEncuestaPk
    {

        [Key]
        [Display(Name = "CompanyOwner")]
        [MaxLength(8)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("COMPANYOWNER")]
        public String Companyowner { get; set; }

        [Key]
        [Display(Name = "Periodo")]
        [MaxLength(6)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("PERIODO")]
        public String Periodo { get; set; }

        [Key]
        [Display(Name = "Secuencia")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("SECUENCIA")]
        public Nullable<Int32> Secuencia { get; set; }

        public HrEncuestaPk()
        {
        }

        public HrEncuestaPk(String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia)
        {
            this.Companyowner = pCompanyowner;
            this.Periodo = pPeriodo;
            this.Secuencia = pSecuencia;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner, Periodo, Secuencia };
            return myObjArray;
        }
    }
}
