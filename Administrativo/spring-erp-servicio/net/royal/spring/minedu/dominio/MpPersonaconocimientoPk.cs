using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio
{
    public class MpPersonaconocimientoPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PERSONA")]
        public Nullable<Int32> IdPersona { get; set; }

            [Key]
            [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
            [Column("ID_CONOCIMIENTO")]
            public Nullable<Int32> IdConocimiento { get; set; }



        public MpPersonaconocimientoPk(){}

        public MpPersonaconocimientoPk(Nullable<Int32> pIdPersona, Nullable<Int32> pIdConocimiento)
        {
            this.IdPersona = pIdPersona;
            this.IdConocimiento = pIdConocimiento;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdPersona, IdConocimiento };
            return myObjArray;
        }
    }
}
