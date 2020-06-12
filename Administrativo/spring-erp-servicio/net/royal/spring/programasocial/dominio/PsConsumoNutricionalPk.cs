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
 * @tabla sgseguridadsys.PS_CONSUMO_NUTRICIONAL
*/
public class PsConsumoNutricionalPk {

	[Key]
	[Display(Name = "ID_INSTITUCION")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_INSTITUCION")]
	public String IdInstitucion  { get; set; }

	[Key]
	[Display(Name = "ID_CONSUMO")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_CONSUMO")]
	public Nullable<Int32> IdConsumo  { get; set; }


        [Key]
	[Display(Name = "ID_CONSUMO_NUTRICIONAL")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_CONSUMO_NUTRICIONAL")]
	public Nullable<Int32> IdConsumoNutricional  { get; set; }

        public PsConsumoNutricionalPk() {
        }

        public PsConsumoNutricionalPk(String pIdInstitucion, Nullable<Int32> pIdConsumo, Nullable<Int32> pIdConsumoNutricional) {

	        this.IdInstitucion = pIdInstitucion;
            this.IdConsumo = pIdConsumo;
	        this.IdConsumoNutricional = pIdConsumoNutricional;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion,IdConsumo,IdConsumoNutricional };
            return myObjArray;
        }
 }
}
