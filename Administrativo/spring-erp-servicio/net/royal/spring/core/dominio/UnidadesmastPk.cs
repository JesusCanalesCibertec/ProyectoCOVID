using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.core.dominio
{

/**
 * 
 * 
 * @tabla dbo.UnidadesMast
*/
public class UnidadesmastPk {

	[Key]
	[Display(Name = "UnidadCodigo")]
	[MaxLength(3)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("UNIDADCODIGO")]
	public String Unidadcodigo  { get; set; }

        public UnidadesmastPk() {
        }

        public UnidadesmastPk(String pUnidadcodigo) {
	this.Unidadcodigo = pUnidadcodigo;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Unidadcodigo };
            return myObjArray;
        }
 }
}
