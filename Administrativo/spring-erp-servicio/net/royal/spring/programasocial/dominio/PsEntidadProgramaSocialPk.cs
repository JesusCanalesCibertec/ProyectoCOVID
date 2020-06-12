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
 * @tabla sgseguridadsys.PS_ENTIDAD_PROGRAMA_SOCIAL
*/
public class PsEntidadProgramaSocialPk {

	[Key]
	[Display(Name = "ID_ENTIDAD")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_ENTIDAD")]
	public Nullable<Int32> IdEntidad  { get; set; }

	[Key]
	[Display(Name = "ID_PROGRAMA_SOCIAL")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_PROGRAMA_SOCIAL")]
	public String IdProgramaSocial  { get; set; }

        public PsEntidadProgramaSocialPk() {
        }

        public PsEntidadProgramaSocialPk(Nullable<Int32> pIdEntidad,String pIdProgramaSocial) {
	this.IdEntidad = pIdEntidad;
	this.IdProgramaSocial = pIdProgramaSocial;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdEntidad,IdProgramaSocial };
            return myObjArray;
        }
 }
}
