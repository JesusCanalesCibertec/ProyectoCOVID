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
 * @tabla dbo.HR_EmpleadoAsistencias
*/
public class HrEmpleadoasistenciasPk {

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
	[Display(Name = "Empleado")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("EMPLEADO")]
	public Nullable<Int32> Empleado  { get; set; }

	[Key]
	[Display(Name = "Secuencia")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("SECUENCIA")]
	public Nullable<Int32> Secuencia  { get; set; }

        public HrEmpleadoasistenciasPk() {
        }

        public HrEmpleadoasistenciasPk(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia) {
	this.Companyowner = pCompanyowner;
	this.Capacitacion = pCapacitacion;
	this.Empleado = pEmpleado;
	this.Secuencia = pSecuencia;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner,Capacitacion,Empleado,Secuencia };
            return myObjArray;
        }
 }
}
