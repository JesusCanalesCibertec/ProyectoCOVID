using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.sistema.dominio
{

/**
 * 
 * 
 * @tabla dbo.SY_Preferences
*/
public class SyPreferencesPk {

	[Key]
	[Display(Name = "Usuario")]
	[MaxLength(20)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("USUARIO")]
	public String Usuario  { get; set; }

	[Key]
	[Display(Name = "Preference")]
	[MaxLength(10)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("PREFERENCE")]
	public String Preference  { get; set; }

        public SyPreferencesPk() {
        }

        public SyPreferencesPk(String pUsuario,String pPreference) {
	this.Usuario = pUsuario;
	this.Preference = pPreference;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Usuario,Preference };
            return myObjArray;
        }
 }
}
