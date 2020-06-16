using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio
{
    public class MpConocimientoPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_CONOCIMIENTO")]
        public Nullable<Int32> IdConocimiento { get; set; }
   

        public MpConocimientoPk(){}

        public MpConocimientoPk(Nullable<Int32> pIdConocimiento)
        {
            this.IdConocimiento = pIdConocimiento;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdConocimiento };
            return myObjArray;
        }
    }
}
