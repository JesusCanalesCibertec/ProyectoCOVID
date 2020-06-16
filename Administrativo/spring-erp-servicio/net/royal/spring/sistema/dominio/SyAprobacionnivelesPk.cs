using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.sistema.dominio
{

/**
 * 
 * 
 * @tabla dbo.SY_AprobacionNiveles
*/
    public class SyAprobacionnivelesPk
    {

	    [Key]
	    [Display(Name = "Codigo")]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("CODIGO")]
	    public Nullable<Int32> Codigo  { get; set; }

        [Key]
        [Display(Name = "COMPANYOWNER")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [MaxLength(8)]
        [Column("COMPANYOWNER")]
        public String CompanyOwner { get; set; }

        public SyAprobacionnivelesPk() { }
        public SyAprobacionnivelesPk(Nullable<Int32> _Codigo, String _CompanyOwner) {
            Codigo = _Codigo;
            CompanyOwner = _CompanyOwner;
         }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Codigo, CompanyOwner };
            return myObjArray;
        }
    }
}
