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
 * @tabla dbo.HR_EncuestaPlantillaDetalle
*/
public class HrEncuestaplantilladetallePk {

	[Key]
	[Display(Name = "Plantilla")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PLANTILLA")]
	public Nullable<Int32> Plantilla  { get; set; }

	[Key]
	[Display(Name = "Pregunta")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PREGUNTA")]
	public Nullable<Int32> Pregunta  { get; set; }

        public HrEncuestaplantilladetallePk() {
        }

        public HrEncuestaplantilladetallePk(Nullable<Int32> pPlantilla,Nullable<Int32> pPregunta) {
	this.Plantilla = pPlantilla;
	this.Pregunta = pPregunta;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Plantilla,Pregunta };
            return myObjArray;
        }
 }
}
