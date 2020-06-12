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
 * @tabla sgseguridadsys.PS_CONSUMO
*/
public class PsConsumoPk {

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

        public PsConsumoPk() {
        }

        public PsConsumoPk(String pIdInstitucion, Nullable<Int32> pIdConsumo) {
	this.IdInstitucion = pIdInstitucion;
	this.IdConsumo = pIdConsumo;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion, IdConsumo };
            return myObjArray;
        }
 }
}
