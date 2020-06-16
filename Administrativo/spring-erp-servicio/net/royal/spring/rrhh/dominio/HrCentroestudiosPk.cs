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
 * @tabla dbo.HR_CentroEstudios
*/
public class HrCentroestudiosPk {

	[Key]
	[Display(Name = "Centro")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("CENTRO")]
	public Nullable<Int32> Centro  { get; set; }

        public HrCentroestudiosPk() {
        }

        public HrCentroestudiosPk(Nullable<Int32> pCentro) {
	this.Centro = pCentro;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Centro };
            return myObjArray;
        }
 }
}
