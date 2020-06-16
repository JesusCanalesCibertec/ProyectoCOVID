using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio
{

/**
 * 
 * 
 * @tabla sgseguridadsys.PS_EMPLEADO
*/
public class PsEmpleadoPk {

	[Key]
	[Display(Name = "ID_INSTITUCION")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_INSTITUCION")]
	public String IdInstitucion  { get; set; }

	[Key]
	[Display(Name = "ID_EMPLEADO")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_EMPLEADO")]
	public Nullable<Int32> IdEmpleado  { get; set; }

        public PsEmpleadoPk() {
        }

        public PsEmpleadoPk(String pIdInstitucion,Nullable<Int32> pIdEmpleado) {
	this.IdInstitucion = pIdInstitucion;
	this.IdEmpleado = pIdEmpleado;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion,IdEmpleado };
            return myObjArray;
        }
 }
}
