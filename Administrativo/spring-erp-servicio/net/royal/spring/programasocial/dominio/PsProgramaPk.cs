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
 * @tabla sgseguridadsys.PS_PROGRAMA
*/
public class PsProgramaPk {

	[Key]
	[Display(Name = "ID_PROGRAMA")]
	[MaxLength(4)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_PROGRAMA")]
	public String IdPrograma  { get; set; }

        public PsProgramaPk() {
        }

        public PsProgramaPk(String pIdPrograma) {
	this.IdPrograma = pIdPrograma;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdPrograma };
            return myObjArray;
        }
 }
}
