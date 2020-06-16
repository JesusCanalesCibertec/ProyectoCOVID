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
 * @tabla sgseguridadsys.PS_INSTITUCION
*/
public class PsInstitucionPk {

	[Key]
	[Display(Name = "ID_INSTITUCION")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_INSTITUCION")]
	public String IdInstitucion  { get; set; }

        public PsInstitucionPk() {
        }

        public PsInstitucionPk(String pIdInstitucion) {
	this.IdInstitucion = pIdInstitucion;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion };
            return myObjArray;
        }
 }
}
