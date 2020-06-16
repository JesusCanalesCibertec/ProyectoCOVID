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
 * @tabla dbo.HR_EncuestaPreguntaValores
*/
public class HrEncuestapreguntavaloresPk {

	[Key]
	[Display(Name = "Pregunta")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PREGUNTA")]
	public Nullable<Int32> Pregunta  { get; set; }

	[Key]
	[Display(Name = "Valor")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("VALOR")]
	public Nullable<Int32> Valor  { get; set; }

        public HrEncuestapreguntavaloresPk() {
        }

        public HrEncuestapreguntavaloresPk(Nullable<Int32> pPregunta,Nullable<Int32> pValor) {
	this.Pregunta = pPregunta;
	this.Valor = pValor;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Pregunta,Valor };
            return myObjArray;
        }
 }
}
