using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.covid.dominio
{
    public class ConocimientoPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_CONOCIMIENTO")]
        public Nullable<Int32> IdConocimiento { get; set; }
   

        public ConocimientoPk(){}

        public ConocimientoPk(Nullable<Int32> pIdConocimiento)
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
