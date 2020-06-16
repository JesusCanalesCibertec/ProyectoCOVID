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
 * @tabla dbo.HR_CapacitacionEval
*/
public class HrCapacitacionevalPk {

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

	[Key]
	[Display(Name = "Evaluacion")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("EVALUACION")]
	public Nullable<Decimal> Evaluacion  { get; set; }

        public HrCapacitacionevalPk() {
        }

        public HrCapacitacionevalPk(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion) {
	this.Companyowner = pCompanyowner;
	this.Capacitacion = pCapacitacion;
	this.Evaluacion = pEvaluacion;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner,Capacitacion,Evaluacion };
            return myObjArray;
        }
 }
}
