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
 * @tabla sgseguridadsys.PS_ENTIDAD
*/
public class PsEntidadPk {

	[Key]
	[Display(Name = "ID_ENTIDAD")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_ENTIDAD")]
	public Nullable<Int32> IdEntidad  { get; set; }

        public PsEntidadPk() {
        }

        public PsEntidadPk(Nullable<Int32> pIdEntidad) {
	this.IdEntidad = pIdEntidad;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdEntidad };
            return myObjArray;
        }
 }
}
