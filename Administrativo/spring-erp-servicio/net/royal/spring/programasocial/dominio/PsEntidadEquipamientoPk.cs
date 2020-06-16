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
 * @tabla sgseguridadsys.PS_ENTIDAD_EQUIPAMIENTO
*/
public class PsEntidadEquipamientoPk {

	[Key]
	[Display(Name = "ID_ENTIDAD")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_ENTIDAD")]
	public Nullable<Int32> IdEntidad  { get; set; }

	[Key]
	[Display(Name = "ID_EQUIPAMIENTO")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_EQUIPAMIENTO")]
	public String IdEquipamiento  { get; set; }

        public PsEntidadEquipamientoPk() {
        }

        public PsEntidadEquipamientoPk(Nullable<Int32> pIdEntidad,String pIdEquipamiento) {
	this.IdEntidad = pIdEntidad;
	this.IdEquipamiento = pIdEquipamiento;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdEntidad,IdEquipamiento };
            return myObjArray;
        }
 }
}
