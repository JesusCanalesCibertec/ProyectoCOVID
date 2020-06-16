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
 * @tabla dbo.HR_CapacitacionPlan
*/
public class HrCapacitacionplanPk {

	[Key]
	[Display(Name = "Companyowner")]
	[MaxLength(8)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("COMPANYOWNER")]
	public String Companyowner  { get; set; }

	[Key]
	[Display(Name = "AnioPlan")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ANIOPLAN")]
	public Nullable<Decimal> Anioplan  { get; set; }

        public HrCapacitacionplanPk() {
        }

        public HrCapacitacionplanPk(String pCompanyowner,Nullable<Decimal> pAnioplan) {
	this.Companyowner = pCompanyowner;
	this.Anioplan = pAnioplan;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner,Anioplan };
            return myObjArray;
        }
 }
}
