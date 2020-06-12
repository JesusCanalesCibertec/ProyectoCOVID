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
 * @tabla dbo.HR_EncuestaPregunta
*/
public class HrEncuestapreguntaPk {

	[Key]
	[Display(Name = "Pregunta")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PREGUNTA")]
	public Nullable<Int32> Pregunta  { get; set; }

        public HrEncuestapreguntaPk() {
        }

        public HrEncuestapreguntaPk(Nullable<Int32> pPregunta) {
	this.Pregunta = pPregunta;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Pregunta };
            return myObjArray;
        }

       
    }
}
