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
public class PsMarcologicoPk {

	[Key]
	[Display(Name = "ID_MARCO_LOGICO")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_MARCO_LOGICO")]
	public String IdMarcologico  { get; set; }

        public PsMarcologicoPk() {
        }

        public PsMarcologicoPk(String pIdMarcologico) {
	this.IdMarcologico = pIdMarcologico;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdMarcologico };
            return myObjArray;
        }
 }
}
