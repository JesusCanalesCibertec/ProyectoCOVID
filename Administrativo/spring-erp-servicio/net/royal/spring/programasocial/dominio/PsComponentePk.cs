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
 * @tabla sgseguridadsys.PS_COMPONENTE
*/
public class PsComponentePk {

	[Key]
	[Display(Name = "ID_COMPONENTE")]
	[MaxLength(4)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_COMPONENTE")]
	public String IdComponente  { get; set; }

        public PsComponentePk() {
        }

        public PsComponentePk(String pIdComponente) {
	this.IdComponente = pIdComponente;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdComponente };
            return myObjArray;
        }
 }
}
