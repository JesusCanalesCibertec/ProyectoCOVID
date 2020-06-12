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
 * @tabla sgseguridadsys.PS_CONSUMO_PLANTILLA
*/
public class PsConsumoPlantillaPk {

	[Key]
	[Display(Name = "ID_INSTITUCION")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_INSTITUCION")]
	public String IdInstitucion  { get; set; }



	[Key]
	[Display(Name = "PLANTILLA")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PLANTILLA")]
	public Nullable<Int32> Plantilla  { get; set; }

        public PsConsumoPlantillaPk() {
        }

        public PsConsumoPlantillaPk(String pIdInstitucion, Nullable<Int32> pPlantilla) {
	this.IdInstitucion = pIdInstitucion;
	this.Plantilla = pPlantilla;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion,Plantilla };
            return myObjArray;
        }
 }
}
