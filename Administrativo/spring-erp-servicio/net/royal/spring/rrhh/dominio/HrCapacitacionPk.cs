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
public class HrCapacitacionPk {

	[Key]
	[Display(Name = "CompanyOwner")]
	[MaxLength(8)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("COMPANYOWNER")]
	public String Companyowner  { get; set; }

	[Key]
	[Display(Name = "Capacitacion")]
	[MaxLength(10)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("CAPACITACION")]
	public String Capacitacion  { get; set; }

        public HrCapacitacionPk() {
        }

        public HrCapacitacionPk(String pCompanyowner,String pCapacitacion) {
	this.Companyowner = pCompanyowner;
	this.Capacitacion = pCapacitacion;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner,Capacitacion };
            return myObjArray;
        }
 }
}
