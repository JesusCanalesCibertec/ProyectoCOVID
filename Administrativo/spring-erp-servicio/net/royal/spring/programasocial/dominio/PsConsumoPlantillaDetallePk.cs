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
public class PsConsumoPlantillaDetallePk {

	[Key]
	[Display(Name = "ID_INSTITUCION")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_INSTITUCION")]
	public String IdInstitucion  { get; set; }



	[Key]
	[Display(Name = "PLANTILLA")]
	[MaxLength(10)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PLANTILLA")]
	public Nullable<Int32> Plantilla  { get; set; }

        [Key]
        [Display(Name = "ID_ITEM")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_ITEM")]
        public String IdItem { get; set; }

        public PsConsumoPlantillaDetallePk() {
        }

        public PsConsumoPlantillaDetallePk(String pIdInstitucion, Nullable<Int32> pPlantilla, String pIdItem) {
	this.IdInstitucion = pIdInstitucion;
	this.Plantilla = pPlantilla;
            this.IdItem = pIdItem;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion,Plantilla,IdItem };
            return myObjArray;
        }
 }
}
