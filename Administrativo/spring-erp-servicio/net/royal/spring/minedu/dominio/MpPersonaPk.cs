using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio
{
    public class MpPersonaPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PERSONA")]
        public Nullable<Int32> IdPersona { get; set; }
   

        public MpPersonaPk(){}

        public MpPersonaPk(Nullable<Int32> pIdPersona)
        {
            this.IdPersona = pIdPersona;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdPersona };
            return myObjArray;
        }
    }
}
