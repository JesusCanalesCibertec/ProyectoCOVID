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
     * @tabla dbo.PersonaMast
*/
    public class PersonamastPk
    {

        [Key]
        [Display(Name = "Persona")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("PERSONA")]
        public Nullable<Int32> Persona { get; set; }

        public PersonamastPk()
        {
        }

        public PersonamastPk(Int32 Persona)
        {
            this.Persona = Persona;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Persona };
            return myObjArray;
        }
    }
}
