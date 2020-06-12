using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.dominio
{

/**
 * 
 * 
 * @tabla dbo.UbicacionGeografica
*/
public class UbicaciongeograficaPk {

	[Key]
	[Display(Name = "UBIGEO")]
	[MaxLength(10)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("UBIGEO")]
	public String Ubigeo  { get; set; }

        public UbicaciongeograficaPk() {
        }

        public UbicaciongeograficaPk(String pUbigeo) {
	this.Ubigeo = pUbigeo;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Ubigeo };
            return myObjArray;
        }
 }
}
