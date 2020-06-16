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
 * @tabla dbo.HR_CapacitacionEvaluacion
*/
public class HrCapacitacionevaluacionPk {

	[Key]
	[Display(Name = "Capacitacion")]
	[MaxLength(10)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("CAPACITACION")]
	public String Capacitacion  { get; set; }

	[Key]
	[Display(Name = "SecuenciaEmpleado")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("SECUENCIAEMPLEADO")]
	public Nullable<Decimal> Secuenciaempleado  { get; set; }

	[Key]
	[Display(Name = "Factor")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("FACTOR")]
	public Nullable<Decimal> Factor  { get; set; }

        public HrCapacitacionevaluacionPk() {
        }

        public HrCapacitacionevaluacionPk(String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor) {
	this.Capacitacion = pCapacitacion;
	this.Secuenciaempleado = pSecuenciaempleado;
	this.Factor = pFactor;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Capacitacion,Secuenciaempleado,Factor };
            return myObjArray;
        }
 }
}
