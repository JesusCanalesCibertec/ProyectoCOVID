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
 * @tabla sgseguridadsys.PS_ITEM
*/
public class PsItemPk {

	[Key]
	[Display(Name = "ID_ITEM")]
	[MaxLength(10)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_ITEM")]
	public String IdItem  { get; set; }

        public PsItemPk() {
        }

        public PsItemPk(String pIdItem) {
	this.IdItem = pIdItem;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdItem };
            return myObjArray;
        }
 }
}
