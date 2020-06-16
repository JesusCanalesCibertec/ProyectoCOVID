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
 * @tabla dbo.HR_EncuestaPlantilla
*/
public class HrEncuestaplantillaPk {

	[Key]
	[Display(Name = "Plantilla")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PLANTILLA")]
	public Nullable<Int32> Plantilla  { get; set; }

        public HrEncuestaplantillaPk() {
        }

        public HrEncuestaplantillaPk(Nullable<Int32> pPlantilla) {
	this.Plantilla = pPlantilla;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Plantilla };
            return myObjArray;
        }
 }
}
