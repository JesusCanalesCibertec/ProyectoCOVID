using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.salud.dominio
{

/**
 * 
 * 
 * @tabla sgseguridadsys.RT_INDICADOR
*/
public class SsCie10capituloPk {

	[Key]
	[Display(Name = "ID_CAPITULO")]
	[MaxLength(15)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_CAPITULO")]
	public String IdCapitulo  { get; set; }

        public SsCie10capituloPk() {
        }

        public SsCie10capituloPk(String pIdCapitulo) {
	    this.IdCapitulo = pIdCapitulo;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdCapitulo };
            return myObjArray;
        }
 }
}
