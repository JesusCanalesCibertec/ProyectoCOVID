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
 * @tabla dbo.HR_Capacitacion_Beneficiario
*/
public class HrCapacitacionBeneficiarioPk {

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
	[Display(Name = "ID_INSTITUCION")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_INSTITUCION")]
	public String IdInstitucion  { get; set; }

	[Key]
	[Display(Name = "ID_BENEFICIARIO")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_BENEFICIARIO")]
	public Nullable<Int32> IdBeneficiario  { get; set; }

        public HrCapacitacionBeneficiarioPk() {
        }

        public HrCapacitacionBeneficiarioPk(String pCompanyowner,String pCapacitacion,String pIdInstitucion,Nullable<Int32> pIdBeneficiario) {
	this.Companyowner = pCompanyowner;
	this.Capacitacion = pCapacitacion;
	this.IdInstitucion = pIdInstitucion;
	this.IdBeneficiario = pIdBeneficiario;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner,Capacitacion,IdInstitucion,IdBeneficiario };
            return myObjArray;
        }
 }
}
