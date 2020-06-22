using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.covid.dominio
{
    public class ResultadoPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("COD_ESTADO")]
        public Nullable<Int32> IdResultado { get; set; }


        public ResultadoPk(){}

        public ResultadoPk(Nullable<Int32> pIdResultado)
        {
            this.IdResultado = pIdResultado;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdResultado };
            return myObjArray;
        }
    }
}
