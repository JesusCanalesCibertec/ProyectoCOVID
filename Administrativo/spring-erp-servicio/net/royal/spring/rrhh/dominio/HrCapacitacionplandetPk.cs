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
 * @tabla dbo.HR_CapacitacionPlanDet
*/
public class HrCapacitacionplandetPk {

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

	[Key]
	[Display(Name = "Capacitacion")]
	[MaxLength(10)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("CAPACITACION")]
	public String Capacitacion  { get; set; }

        public HrCapacitacionplandetPk() {
        }

        public HrCapacitacionplandetPk(String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion) {
	this.Companyowner = pCompanyowner;
	this.Anioplan = pAnioplan;
	this.Capacitacion = pCapacitacion;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner,Anioplan,Capacitacion };
            return myObjArray;
        }
 }
}
